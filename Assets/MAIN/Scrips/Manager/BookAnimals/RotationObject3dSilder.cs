using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;

public class RotationObject3dSilder : MonoBehaviour
{
    public Slider sliderY;
    public RectTransform objectRotation;

    void Start()
    {
        sliderY.onValueChanged.AddListener((v) =>
        {
           
            objectRotation.transform.rotation = Quaternion.Euler(objectRotation.transform.rotation.x, 360 * v, objectRotation.transform.rotation.z);
        });
       
    }
   

}
