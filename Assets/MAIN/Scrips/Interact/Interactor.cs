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
    public Manager_UI managerUI;

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
        
        managerUI.groupDetailPanelAnimal.GetComponent<DetailPanelAnimal>().configCage = configCage;
        managerUI.OpenModalDetailPanel();
    }
    #endregion

    #region TRIGGER CAGE
    public void OnTriggerEnterCage(object data)
    {
        if (data == null)
        {
            managerUI.OpenModalInteract();
            return;
        }
        GameObject temp = (GameObject)data;
        configCage = temp.GetComponent<ConfigCage>();
       managerUI.OpenModalInteract();
        SetDataToButtonFood();
       
    }
    public void OnTriggerExitCage(object data = null)
    {
        configCage = null;
        managerUI.CloseModalInteract();
        ResetDataButtonFood();
    }
    #endregion
    

    #region Event UI Interact Cage
    public void OpenViewAnimals()//use it for button see detail animal in cage
    {
        managerUI.OpenModalViewAnimals();
        configCage.OpenViewCage();
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
        managerUI.CloseModalViewAnimals();
        configCage.CloseViewCage();
    }
    #endregion

    #region Set & Refresh Data
    private void SetDataToButtonFood()
    {
        try
        {
            SOAnimal dataAnimal = configCage.SoAnimal;

            for (int i = 0; i < managerUI.btnFoods.Count; i++)
            {
                GameObject obj = managerUI.btnFoods[i];
                //set Icon. Event spam food
                obj.GetComponent<Image>().sprite = dataAnimal.dataFoods.SoFoods[i].iconFood;
                obj.GetComponent<Button>().onClick.AddListener(() =>
                {
                    configCage.foodStorage.SpamwnFood(i);
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
            for (int i = 0; i < managerUI.btnFoods.Count; i++)
            {
                GameObject obj = managerUI.btnFoods[i];
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
    public void EatFoodAnimal()
    {
        configCage.foodStorage.SpamwnFood(1);  
    }

}
