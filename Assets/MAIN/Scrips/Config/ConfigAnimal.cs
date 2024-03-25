using System;
using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
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
    public GameObject prefabEmoji;
    public STATE_ANIMAL stateAnimal;
    #endregion
    #region COMPONENTS CONTROL
    [HideInInspector] public Animator animator;
    [HideInInspector] public NavMeshAgent agent;
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

    #endregion
    #region BOOLEAN

    public bool CanMeetingAnimal = true;
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
  

    
    private void Update()
    {
        UpdateStateAnimal();
    }

  
    #endregion

    public GameObject ObjectEmojiStatus(GameObject _ParentObj)
    {
        GameObject newObject = Instantiate(prefabEmoji,_ParentObj.transform);
 /*       newObject.transform.SetParent(_ParentObj.transform);
        newObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        newObject.transform.position = new Vector3(0, 0, 0);*/
        //newObject.transform.localScale = Vector3.one; // Reset scale to (1, 1, 1)

        return newObject;
    }

    public void UpdateStateAnimal()
    {

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
