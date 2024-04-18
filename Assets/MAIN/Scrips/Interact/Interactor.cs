using System;
using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using Unity.VisualScripting;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{

 //   [HideInInspector]
    public ConfigCage configCage;   
    private void Start()
    {
        //-- INTERACT CAGE --
        this.Register(EventID.OpenInteractCage, OnTriggerEnterCage);
        this.Register(EventID.CloseInteractCage, OnTriggerExitCage);

      
    }
    private void OnDestroy()
    {
        this.Unregister(EventID.OpenInteractCage, OnTriggerEnterCage);
        this.Unregister(EventID.CloseInteractCage, OnTriggerExitCage);
    }
    #region TRIGGER DETAIL PANEL ANIMAL
    public void OpenDetailPanelAnimal()
    {
        Manager_UI.Instance.OpenViewDetailAnimal(configCage);
    }
    #endregion

    #region TRIGGER CAGE
    public void OpenQuestToOpenCage()
    {
        QuestController.Instance.OpenModal(
        () =>//affter success
        {
            StartCoroutine(Common.delayCoroutine(1f, () =>
            {
                configCage.SoAnimal.IsLock = false;
                setUIcageInteract();
                configCage.UnClockCage();
                Debug.Log("OPEN SUCCESS CAGE ANIMAL :" + configCage.SoAnimal.name);
            }));
            
        },
        () =>//affter failed
        {
            
        }
        );
    }
    private void setUIcageInteract()
    {
        bool isCageLocked = configCage.SoAnimal.IsLock;
        
        if (isCageLocked)
            Manager_UI.Instance.OpenUnClockCage();
        else
            Manager_UI.Instance.OpenInteractCage();


    }
    public void OnTriggerEnterCage(object data)
    {
        if (data == null)
        {
            Debug.LogError("NOT FOUND SO CONFIG CAGE");
            return;
        }
        GameObject temp = (GameObject)data;
        configCage = temp.GetComponent<ConfigCage>();
        setUIcageInteract();
        SetDataToButtonFood();
       
    }
    public void OnTriggerExitCage(object data = null)
    {
        configCage = null;
        Manager_UI.Instance.CloseAllModal();
        ResetDataButtonFood();
    }
    #endregion
    

    #region Event UI Interact Cage
    public void OpenViewAnimals()//use it for button see detail animal in cage
    {
        Manager_UI.Instance.OpenViewAnimal();
        configCage.OpenViewCage(); //turn on cammera in cage
    }
    #endregion
    #region Event UI View Animals
    public void SwitchNextAnimal() {
        configCage.SwitchToNextTarget();
    }
    public void SwitchPreviousAnimal() {
        configCage.SwitchToPreviousTarget();
    }
    public void OutViewAnimal() {
        Manager_UI.Instance.CloseModalViewAnimals();
        configCage.CloseViewCage(); //turn off cammera in cagef
    }
    #endregion

    #region Set & Refresh Data
    private void SetDataToButtonFood()
    {
        try
        {
            SOAnimal dataAnimal = configCage.SoAnimal;
            
            for (int i = 0; i < Manager_UI.Instance.btnFoods.Count; i++)
            {
                int currenIndex = i;
                //set Icon. Event spam food
                Manager_UI.Instance.btnFoods[i].GetComponent<Image>().sprite = dataAnimal.dataFoods.SoFoods[i].iconFood;
                Manager_UI.Instance.btnFoods[i].GetComponent<Button>().onClick.AddListener(() =>
                {
                    
                    configCage.foodStorage.SpamwnFood(currenIndex);
                });
            }
        }
        catch (Exception)
        {
            Debug.LogError("Something wrong in SetDataButtonFood");
        }
    }
    private void ResetDataButtonFood()
    {
        try
        {
            for (int i = 0; i < Manager_UI.Instance.btnFoods.Count; i++)
            {
                GameObject obj = Manager_UI.Instance.btnFoods[i];
                //reset Icon. Event spam food
                obj.GetComponent<Image>().sprite = null;
                obj.GetComponent<Button>().onClick.RemoveAllListeners();
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    #endregion



    [ProButton]
    public void EatFoodAnimal() //DEBUG BUTTON
    {
        configCage.foodStorage.SpamwnFood(1);  
    }

}
