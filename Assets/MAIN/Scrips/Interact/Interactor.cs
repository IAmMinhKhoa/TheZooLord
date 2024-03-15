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
    private Manager_UI managerUI;

    [SerializeField] private int _numFound;

    [HideInInspector]
    public ConfigCage configCage;
    private void Start()
    {
        managerUI =GetComponent<Manager_UI>();
        PointInteract.OnEnterTrigger += PointInteract_OnEnterTrigger ;
        PointInteract.OnExitTrigger += PointInteract_OnExitTrigger; ;

    }

    protected void ViewAnimals()
    {
        managerUI.groupInteractCage.SetActive(false);
        configCage.ObserveCage();
    }

    private void PointInteract_OnEnterTrigger(ConfigCage obj)
    {
        configCage = obj;
        managerUI.OpenModalInteract();
        for (int i = 0; i < managerUI.dataButtons.Count; i++)
        {
            Debug.Log(i + "/" + managerUI.dataButtons.Count);
            managerUI.dataButtons[i].btn.onClick.AddListener(() => FeedAnimalInteract(i));
        }

        managerUI.BtnObseverCage.onClick.AddListener(ViewAnimals);
    }

    private void PointInteract_OnExitTrigger()
    {
        configCage = null;
        managerUI.CloseModalInteract();
        managerUI.BtnObseverCage.onClick.RemoveAllListeners();
        for (int i = 0; i < managerUI.dataButtons.Count; i++)
        {
          
            managerUI.dataButtons[i].btn.onClick.RemoveAllListeners();
        }
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
            Debug.Log("eat di");
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
