using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using com.cyborgAssets.inspectorButtonPro;
using Unity.Collections;
using UnityEngine;

public class ZooManager : MonoBehaviour
{
    public  bool isDay=true;
    public static event Action<bool> SetStateDayNight;


    public float TimeLoopDayNight = 60f;

    private float currentTime = 0;

    [Header("Gradients")]
    [SerializeField] private Gradient fogGradient;
    [SerializeField] private Gradient ambientGradient;
    [SerializeField] private Gradient directionLightGradient;
    [SerializeField] private Gradient skyboxTintGradient;

    [Header("Enviromental Assets")]
    [SerializeField] private Light directionalLight;
    [SerializeField] private Material skyboxMaterial;
    [SerializeField] private float rotationSpeed = 1f;

    float sunPosition = 0;

    private void Update()
    {
        UpdateTime();
        UpdateDayNightCycle();
        RotateSkybox();
    }

    private void UpdateTime()
    {
        currentTime += Time.deltaTime / TimeLoopDayNight;
        currentTime = Mathf.Repeat(currentTime, 1f);
        if(currentTime>=0.25f &&currentTime<0.7f)
        {
            isDay = false;
            SetStateDayNight?.Invoke(isDay);
        }
        else if(currentTime>0.7f)
        { 
            isDay = true;
            SetStateDayNight?.Invoke(isDay);
        }
        if (!isDay) currentTime += Time.deltaTime / 20;
       // Debug.Log(currentTime);

    }
    private void UpdateDayNightCycle()
    {
        sunPosition = Mathf.Repeat(currentTime + 0.25f, 1f);
        directionalLight.transform.rotation = Quaternion.Euler(sunPosition * 360f, 0f, 0f);
        RenderSettings.fogColor = fogGradient.Evaluate(currentTime);
        RenderSettings.ambientLight = ambientGradient.Evaluate(currentTime);
        directionalLight.color = directionLightGradient.Evaluate(currentTime);
        skyboxMaterial.SetColor("_Tint", skyboxTintGradient.Evaluate(currentTime));
    }

    private void RotateSkybox()
    {
        float currentRotation = skyboxMaterial.GetFloat("_Rotation");
        float newRotation = currentRotation + rotationSpeed * Time.deltaTime;
        newRotation = Mathf.Repeat(newRotation, 360f);
        skyboxMaterial.SetFloat("_Rotation", newRotation);
    }

    private void OnApplicationQuit()
    {
        skyboxMaterial.SetColor("_Tint", new Color(0.5f, 0.5f, 0.5f));
    }



    [ProButton]
    public void ChangeToDay()
    {

        currentTime = 0.7f;
    }
    [ProButton]
    public void ChangeToNight()
    {
        currentTime = 0.25f;
    }

}
