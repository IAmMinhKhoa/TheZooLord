using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class PointInteract : MonoBehaviour
{
    public ConfigCage config;
    public static event Action<ConfigCage> OnEnterTrigger;
    public static event Action OnExitTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //call event to ANIMAL CONTROLLER -> check CanInteract=TRUE in configAnimal
            OnEnterTrigger?.Invoke(config);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //call event to ANIMAL CONTROLLER -> check CanInteract=FALSE in configAnimal
            OnExitTrigger?.Invoke();

           
        }
    }
}
