using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius;
    [SerializeField] private LayerMask _interactableMask;

    private readonly Collider[] _collider = new Collider[3];
    //-----------

    public Manager_UI managerUI;

    [SerializeField] private int _numFound;

    [HideInInspector]
    public ConfigCage configCage;
    private void Start()
    {
        this.Register(EventID.OpenInteractCage, OnTriggerEnterCage);
        this.Register(EventID.CloseInteractCage, OnTriggerExitCage);
    }


    public void OnTriggerEnterCage(object data)
    {
        configCage = (ConfigCage)data;
        managerUI.OpenModalInteract();
    }
    public void OnTriggerExitCage(object data = null)
    {
        configCage = null;
        managerUI.CloseModalInteract();
    }

    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _collider, _interactableMask);

        if (_numFound > 0)
        {
            var interactable = _collider[0].GetComponent<IInteractTable>();

            if(interactable!=null && Keyboard.current.eKey.wasPressedThisFrame)
            {
                interactable.Interact(this);
            }
        }
    }

    //function set in button interact when feed for animal
    public void FeedAnimalInteract(int index)
    {
        
        if (configCage != null)
        {
            configCage.foodStorage.SpamwnFood(index);
        }
        else
        {
            Debug.Log("Null: " + configCage );
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
