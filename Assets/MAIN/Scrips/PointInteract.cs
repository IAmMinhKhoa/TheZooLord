using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInteract : MonoBehaviour
{
    public ConfigAnimal config;
    public event Action OnEnterTrigger;
    public event Action OnExitTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //call event to ANIMAL CONTROLLER -> check CanInteract=TRUE in configAnimal
            OnEnterTrigger?.Invoke();

            //Set confiAnimal of that animal to INTERACTOR in player 
            Interactor interactPlayer = other.gameObject.GetComponent<Interactor>();
            if(interactPlayer != null)
            {
                interactPlayer.configAnimal = config;
            }
            else {
                Debug.Log("Can't get Interactor in " + other.gameObject);
            }

            Debug.Log("Touch");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //call event to ANIMAL CONTROLLER -> check CanInteract=FALSE in configAnimal
            OnExitTrigger?.Invoke();

            //DELETE confiAnimal of that animal to INTERACTOR in player 
            Interactor interactPlayer = other.gameObject.GetComponent<Interactor>();
            if (interactPlayer != null)
            {
                interactPlayer.configAnimal = null;
            }
            else
            {
                Debug.Log("Can't get Interactor in " + other.gameObject);
            }

            Debug.Log("Exit");
        }
    }
}
