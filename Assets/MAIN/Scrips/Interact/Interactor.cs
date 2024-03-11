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
    [SerializeField] private int _numFound;

    [HideInInspector]
    public ConfigAnimal configAnimal;
    private void Start()
    {
        PointInteract.OnEnterTrigger += PointInteract_OnEnterTrigger1; ;
        PointInteract.OnExitTrigger += PointInteract_OnExitTrigger; ;
    }

    private void PointInteract_OnEnterTrigger1(ConfigAnimal obj)
    {
        configAnimal = obj;
    }

    private void PointInteract_OnExitTrigger()
    {
        configAnimal = null;
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
    public void FeedAnimalInteract(string index)
    {
        if (configAnimal != null)
        {
            int parsedIndex;
            if (int.TryParse(index, out parsedIndex)) configAnimal.foodStorage.SpamwnFood(parsedIndex);
            else  Debug.Log("Invalid index: " + index);
        }
        else
        {
            Debug.Log("Null: " + configAnimal);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
