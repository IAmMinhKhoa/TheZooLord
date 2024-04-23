using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class Track : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] VideoPlayer video;
    Slider tracking;
    bool slide = false;

    private void Start()
    {
        tracking = GetComponent<Slider>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        video.Pause();
        slide = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        video.Play();
        float frame = (float)tracking.value * (float)video.frameCount;
        video.frame = (long)frame;
        slide = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(!slide)
        {
            tracking.value = (float)video.frame / (float)video.frameCount;
        }        
    }
}
