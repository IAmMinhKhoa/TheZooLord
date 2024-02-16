using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZooManager : MonoBehaviour
{
    public  bool DAY=true;
    public static event Action<bool> SetStateDayNight;


    public float TimeLoopDayNight = 5f;
    private float SaveTimeLoopDefault;
    private void Start()
    {
        SaveTimeLoopDefault = TimeLoopDayNight;
    }
    private void Update()
    {
        TimeLoopDayNight -= Time.deltaTime;
        if (TimeLoopDayNight < 0)
        {
            TimeLoopDayNight = SaveTimeLoopDefault;
            DAY = !DAY;
            SetStateDayNight?.Invoke(DAY);
        }
        
    }
}
