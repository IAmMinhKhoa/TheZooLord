using System;
using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using UnityEditorInternal.VersionControl;
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

 //   [HideInInspector]
    public ConfigCage configCage;
  //  [HideInInspector]
    public GiftController giftControl;
    private void Start()
    {
        //-- INTERACT CAGE --
        this.Register(EventID.OpenInteractCage, OnTriggerEnterCage);
        this.Register(EventID.CloseInteractCage, OnTriggerExitCage);
        //-- INTERACT CAGE --
        this.Register(EventID.OpenInteractGift, OnTriggerEnterGift);
        this.Register(EventID.CloseInteractGift, OnTriggerExitGift);
    }
    private void OnDestroy()
    {
        this.Unregister(EventID.OpenInteractCage, OnTriggerEnterCage);
        this.Unregister(EventID.CloseInteractCage, OnTriggerExitCage);

        this.Unregister(EventID.OpenInteractGift, OnTriggerEnterGift);
        this.Unregister(EventID.CloseInteractGift, OnTriggerExitGift);
    }

    #region TRIGGER CAGE
    public void OnTriggerEnterCage(object data)
    {
        GameObject temp = (GameObject)data;
        configCage = temp.GetComponent<ConfigCage>();
        managerUI.OpenModalInteract();
    }
    public void OnTriggerExitCage(object data = null)
    {
        configCage = null;
        managerUI.CloseModalInteract();
    }
    #endregion
    #region TRIGGER GIFT 
    public void OnTriggerEnterGift (object data)
    {
        GameObject temp = (GameObject)data;
        giftControl = temp.GetComponent<GiftController>();
        giftControl.OpenModal();
    }
    public void OnTriggerExitGift(object data = null)
    {
        giftControl.CloseModal();
        giftControl = null;
    }
    #endregion 

    #region Event UI Interact Cage
    public void OpenViewAnimals()
    {
        managerUI.OpenModalViewAnimals();
        configCage.OpenViewCage();
    }
    #endregion
    #region Event UI View Animals
    public void SwitchNextAnimal() {
        configCage.SwitchToNextTarget();
    }
    public void SwitchPreviousAnimal() {
        configCage.SwitchToPreviousTarget();
    }
    public void OutViewAnimal() {
        managerUI.CloseModalViewAnimals();
        configCage.CloseViewCage();
    }
    #endregion
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

    [ProButton]
    public void EatFoodAnimal()
    {
        configCage.foodStorage.SpamwnFood(1);  
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
