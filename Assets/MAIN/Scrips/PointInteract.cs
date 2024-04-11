using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class PointInteract : MonoBehaviour
{
    public GameObject ObjectData;
    public EventID EventIdOnTrigger;
    public EventID EventIdExitTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.Broadcast(EventIdOnTrigger, ObjectData);
            Debug.Log("trigger:" + ObjectData);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.Broadcast(EventIdExitTrigger);
            Debug.Log("OUT trigger:" + ObjectData);
        }
    }
   
}
