using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class PointInteract : MonoBehaviour
{
    public ConfigCage configCage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.Broadcast(EventID.OpenInteractCage, configCage);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.Broadcast(EventID.CloseInteractCage);
        }
    }
}
