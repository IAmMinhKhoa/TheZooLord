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
        Hungry, //foodIndex <100
        NotHungry, //foodIndex = 100
        Sleep,
        FeedAnimal,

        Other
    }
    public STATE_ANIMAL stateAnimal;
    #endregion
    #region COMPONENTS CONTROL
    public Animator animator;
    public NavMeshAgent agent;
    #endregion
    #region PARAMETER OF ANIMAL
    public int FoodIndex=90;
    public int foodIndex
    {
        get { return FoodIndex; }
        set
        {
            if (value > 100) FoodIndex = 100;
            else FoodIndex = value;

           // if (value > 50 || value <= 100) stateAnimal = STATE_ANIMAL.NotHungry;
           // else stateAnimal = STATE_ANIMAL.Hungry  ;
        }
    }
    #endregion
    #region BOOLEAN
    public bool CanInteract = false;

    #endregion
    private void Update()
    {
        if(foodIndex<50 && stateAnimal!= STATE_ANIMAL.FeedAnimal) stateAnimal = STATE_ANIMAL.Hungry;

    }
    public STATE_ANIMAL getStateAnimal()
    {
        if(foodIndex > 50 && foodIndex <= 100) return stateAnimal = STATE_ANIMAL.NotHungry;
        else return stateAnimal = STATE_ANIMAL.Hungry;
    }
    #region LIFE CYCLE GAMEOBJECT

    private void Start()
    {
        foodIndex = 90;
        ZooManager.SetStateDayNight += ZooManager_SetStateDayNight;
    }

    private void ZooManager_SetStateDayNight(bool day)
    {
        if (!day) stateAnimal = STATE_ANIMAL.Sleep;
        else stateAnimal = STATE_ANIMAL.Other;
    }


    #endregion
















    #region Template
    #endregion


}
