using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.UI;
using static UnityEngine.Rendering.DebugUI;

public class ConfigAnimal :  MonoBehaviour
{
    #region ENUM
    public enum STATE_ANIMAL
    {
        Other,
        Hungry, //foodIndex <100
        NotHungry, //foodIndex = 100
        Sleep,
        FeedAnimal,
        Meeting
        
    }
    public STATE_ANIMAL stateAnimal;
    #endregion
    #region COMPONENTS CONTROL
    [HideInInspector] public Animator animator;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public FoodStorage foodStorage;
    #endregion
    #region PARAMETER OF ANIMAL
    public int FoodIndex;
    public int foodIndex
    {
        get { return FoodIndex; }
        set
        {
            if (value > 100) FoodIndex = 100;
            else FoodIndex = value;
        }
    }
    public float rangerInteractWithAnimal;
    public LayerMask layerAnimalInteract;
    #endregion
    #region BOOLEAN
    public bool CanInteract = false;

    #endregion
    #region List

    #endregion

    #region LIFE CYCLE & FUNCTION
    private void Start()
    {
        ZooManager.SetStateDayNight += ZooManager_SetStateDayNight;
    }
    private void Update()
    {
        if (foodIndex < 50 && stateAnimal != STATE_ANIMAL.FeedAnimal) stateAnimal = STATE_ANIMAL.Hungry;

    }

    private void ZooManager_SetStateDayNight(bool day)
    {
        if (!day) stateAnimal = STATE_ANIMAL.Sleep;
        else stateAnimal = STATE_ANIMAL.Other;
    }
    public STATE_ANIMAL getStateAnimal()
    {
        if (foodIndex > 50 && foodIndex <= 100) return stateAnimal = STATE_ANIMAL.NotHungry;
        else return stateAnimal = STATE_ANIMAL.Hungry;
    }

    #endregion


    #region DrawRanger
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangerInteractWithAnimal); //Gizmos ranger
    }
    #endregion













    #region Template
    #endregion


}
