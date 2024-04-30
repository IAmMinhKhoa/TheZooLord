using com.cyborgAssets.inspectorButtonPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public bool isDay = true;
    #region SINGLOTEN
    public static DayNightCycle Instance { get; private set; }
    #endregion

    #region VARIABLE DAY ENVIRONMENT
    [SerializeField] private Light sun;

    [SerializeField, Range(0, 24)] private float timeOfDay;

    [SerializeField] private float sunRotationSpeed;

    [Header("LightingPreset")]
    [SerializeField] private Gradient skyColor;
    [SerializeField] private Gradient equatorColor;
    [SerializeField] private Gradient sunColor;
    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes if needed
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    private void Update()
    {
        timeOfDay += Time.deltaTime * sunRotationSpeed;
        if(timeOfDay > 24)
        {
            timeOfDay = 0;
        }
        if(timeOfDay >= 6 && timeOfDay < 19)
        {
            isDay = true;
        } else
        {
            isDay = false;
        }
        UpdateSunRotation();
        UpdateLighting();
    }
    private void OnValidate()
    {
        UpdateSunRotation();
        UpdateLighting();
    }

    #region DAY AND NIGHT
    private void UpdateSunRotation()
    {
        float sunRotation = Mathf.Lerp(-90, 270, timeOfDay / 24);
        sun.transform.rotation = Quaternion.Euler(sunRotation, sun.transform.rotation.y, sun.transform.rotation.z);
    }

    private void UpdateLighting()
    {
        float timeFraction = timeOfDay / 24;
        RenderSettings.ambientEquatorColor = equatorColor.Evaluate(timeFraction);
        RenderSettings.ambientSkyColor = skyColor.Evaluate(timeFraction);
        sun.color = sunColor.Evaluate(timeFraction);
    }
    #endregion

    [ProButton]
    public void ChangeToDay()
    {

        timeOfDay = 6f;
    }
    [ProButton]
    public void ChangeToNight()
    {
        timeOfDay = 19f;
    }
}
