using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackLevel : MonoBehaviour
{
    [SerializeField] Canvas levelCanvas;
    [SerializeField] GameObject parentObject;

    private void Awake()
    {
        parentObject = transform.parent.gameObject;
    }

    private void OnMouseDown()
    {
        parentObject.SetActive(false); 
        levelCanvas.gameObject.SetActive(true);
    }
}
