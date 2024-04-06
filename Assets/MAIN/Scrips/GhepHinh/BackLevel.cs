using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackLevel : MonoBehaviour
{
    [SerializeField] Canvas levelCanvas;
    [SerializeField] GameObject parentObject;

    [SerializeField] ParticleSystem winVFX;

    private void Awake()
    {
        parentObject = transform.parent.gameObject;
    }

    private void OnMouseDown()
    {
        //winVFX.Stop(); 
        winVFX.Clear();
        parentObject.SetActive(false); 
        levelCanvas.gameObject.SetActive(true);

    }
}
