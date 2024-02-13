using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ConfigAnimal : MonoBehaviour
{
    #region ENUM
    public enum STATE_ANIMAL
    {
        PoinInteract,
        Eat,




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
    public bool EatingAction = false;
    #endregion





    #region Template
    #endregion


}
