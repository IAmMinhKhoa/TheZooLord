using System;
using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.UI;
using static UnityEngine.Rendering.DebugUI;

public class ConfigAnimal :  MonoBehaviour
{
    #region ENUM
    public enum STATE_ANIMAL
    {
        None,
        MoveAround,
        Idle,
        Hungry, //foodIndex <100
        NotHungry, //foodIndex = 100
        Sleep,
        Eat,
        MeetingAnimal,
        MeetingPlayer
        
    }
    public STATE_ANIMAL stateAnimal;
    #endregion
    #region COMPONENTS CONTROL
    [HideInInspector] public Animator animator;
    [HideInInspector] public NavMeshAgent agent;
    #endregion
    #region ACTION

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
    public int TimeCDMeeting;
    public LayerMask layerAnimalInteract;
    #endregion
    #region BOOLEAN

    public bool CanMeetingAnimal = true;
    #endregion
    #region List

    #endregion

    #region LIFE CYCLE & FUNCTION
    private void Start()
    {
        StartCoroutine(AutoDecreaseFood());

    }

    protected IEnumerator AutoDecreaseFood()
    {
        yield return new WaitForSeconds(1f);
      

        while (true)
        {
            yield return new WaitForSeconds(1f); 

            if (foodIndex <= 0) foodIndex = 0;
            else foodIndex -= 1;
        }
    }
  
    public void ToogleCanMeetingAnimal(int time)
    {
        StartCoroutine(CoroutineToogleCanMeetingAnimal(time));
    }
    private IEnumerator CoroutineToogleCanMeetingAnimal(int time)
    {
        CanMeetingAnimal = false;
        yield return new WaitForSeconds(time);
        CanMeetingAnimal = true;
    }
    
    private void Update()
    {
        UpdateStateAnimal();
    }

   protected IEnumerator CDToTime(int timeCD,bool setBool)
    {
        yield return new WaitForSeconds(timeCD);
        CanMeetingAnimal = setBool;
    }
    #endregion



    public void UpdateStateAnimal()
    {
       // Debug.Log(ZooManager.isDay);
        if (!ZooManager.isDay) stateAnimal = STATE_ANIMAL.Sleep;
        else if (foodIndex <= 50) stateAnimal = STATE_ANIMAL.Hungry;
        else if (CanMeetingAnimal) stateAnimal = STATE_ANIMAL.MeetingAnimal;
        else stateAnimal = STATE_ANIMAL.MoveAround;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            Debug.Log("khoa cham");
            CanMeetingAnimal = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            CanMeetingAnimal = false;
        }
    }
    #region DrawRanger
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangerInteractWithAnimal); //Gizmos ranger
    }
    #endregion



    [ProButton]
    public void chanegStateHungry()
    {
        stateAnimal = ConfigAnimal.STATE_ANIMAL.Hungry;
    }
    [ProButton]
    public void chanegStateSleep()
    {
        stateAnimal = ConfigAnimal.STATE_ANIMAL.Sleep;
    }
    [ProButton]
    public void chanegStateWalkAround()
    {
        stateAnimal = ConfigAnimal.STATE_ANIMAL.MoveAround;
    }
    [ProButton]
    public void chanegStateMeeting()
    {
        stateAnimal = ConfigAnimal.STATE_ANIMAL.MeetingAnimal;
    }
   
    







    #region Template
    #endregion


}
