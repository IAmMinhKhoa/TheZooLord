using System;
using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class ZooManager : MonoBehaviour
{
    public  bool DAY=true;
    public static event Action<bool> SetStateDayNight;


    public float TimeLoopDayNight = 60f;
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

    [ProButton]
    public void ChangeToDay()
    {
        SaveTimeLoopDefault = TimeLoopDayNight;
        DAY = true;
    }
    [ProButton]
    public void ChangeToNight()
    {
        SaveTimeLoopDefault = TimeLoopDayNight;
        DAY = false;
    }
}
