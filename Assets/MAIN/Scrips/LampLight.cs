using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampLight : MonoBehaviour
{
    [SerializeField] GameObject lampDefault;
    [SerializeField] GameObject lampLight;

    private void Start()
    {
        SetLampDay();

    }
    private void Update()
    {
        if(DayNightCycle.Instance.isDay)
        {
            SetLampDay();
        } else
        {
            SetLampNight();
        }
    }

    private void SetLampDay()
    {
        lampDefault.SetActive(true);
        lampLight.SetActive(false);
    }

    private void SetLampNight()
    {
        lampLight.SetActive(true);
        lampDefault.SetActive(false);
    }
}
