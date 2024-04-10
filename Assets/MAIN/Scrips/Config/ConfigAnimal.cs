using System;
using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Rendering;
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
    }
    public STATE_ANIMAL stateAnimal;
    [HideInInspector] public STATE_ANIMAL previousState = STATE_ANIMAL.None;
    #endregion
    public List<SOEmoji> SO_Emojis = new List<SOEmoji>();
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

    public GameObject ObjectEmojiStatus(STATE_ANIMAL state,GameObject _ParentObj)
    {
        foreach (var emoji in SO_Emojis)
        {
            if (emoji.nameEmoji == state.ToString())
            {
                GameObject prefabEmoji = emoji.GetPrefabEmoji();
                GameObject newObject = Instantiate(prefabEmoji, _ParentObj.transform);
                Debug.Log("Spawn Prefab Emoji" + emoji.nameEmoji);
                return newObject;
            }
        }
        Debug.LogError("Not have emoji " + state + "in SO_Emojis");
        return null;

    }

    
    public void DestroyChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
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



 
    







    #region Template
    #endregion


}
