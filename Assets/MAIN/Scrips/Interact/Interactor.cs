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

    //[HideInInspector]
    public ConfigAnimal configAnimal;

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
    public void FeedAnimalInteract()
    {
        if (configAnimal != null)
        {
            //check value food, if hungry -> state.FEEDANIMAL else noHungry->default state
            if (configAnimal.getStateAnimal()==ConfigAnimal.STATE_ANIMAL.Hungry) configAnimal.stateAnimal = ConfigAnimal.STATE_ANIMAL.FeedAnimal;
            else if (configAnimal.getStateAnimal() == ConfigAnimal.STATE_ANIMAL.NotHungry) configAnimal.stateAnimal = ConfigAnimal.STATE_ANIMAL.NotHungry;

        }
        else Debug.Log("Null : " + configAnimal);   


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
