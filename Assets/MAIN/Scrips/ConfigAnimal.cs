using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ConfigAnimal : MonoBehaviour
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
    public int foodIndex = 100;
    #endregion

    #region BOOLEAN
    public bool CanInteract = false;
    public bool EatAction = false;
    #endregion





    #region Template
    #endregion


}
