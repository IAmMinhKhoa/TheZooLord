using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampLight : MonoBehaviour
{
    [SerializeField] GameObject lampDefault;
    [SerializeField] GameObject lampLight;
    bool temp = true;
    private void Start()
    {
        SetLampDay();

    }
    private void Update()
    {
        if(DayNightCycle.Instance.isDay&& temp==true)
        {
            SetLampDay();
            temp = false;
        } else if(!DayNightCycle.Instance.isDay && temp == false)
        {
            SetLampNight();
            temp = true;
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
