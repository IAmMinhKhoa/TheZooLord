
using System;
using System.Collections;
using UnityEngine;

public class Common 
{
    public static IEnumerator delayCoroutine(float delay, Action action)
    {
        yield return Delay(delay);
        action?.Invoke();
        yield return action;
    }
    public static IEnumerator Delay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
    
}
