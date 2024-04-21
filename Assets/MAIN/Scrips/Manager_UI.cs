using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Manager_UI : MonoBehaviour
{
    #region singleton
    public static Manager_UI Instance
    {
        get;
        private set;
    }
    #endregion
    [Header("MAIN UI CHARACTER")]
    public GameObject groupDetailPanelAnimal;
    public GameObject groupViewAnimalsUI;
    public GameObject groupInteractCageUI;
    public GameObject groupOverlayUI;
    //---UI child in InteractCage
    public GameObject GroupUnClockCage;
    public GameObject GroupInteractToOpenCage;
    [Header("LIST UI BUTTON OF INTERACT")]
    public List<GameObject> btnFoods;

   //--- USE IN LOCAL ---
    private bool toggleBtnFoods=false;
    private bool toggleBtnMiniMap = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

    }
   
    protected void SetModalActive(GameObject obj,bool boolean)
    {
        obj.SetActive(boolean);
    }
   

  
    #region TESTING
    
    #endregion




    //-------------- refactory UI ---------------

    #region CLOSE MODAL
    public void CloseModalViewAnimals()
    {
        CloseAllModal();
        SetModalActive(groupInteractCageUI, true);
    }
    public void CloseModalViewDetailAnimal()
    {
        CloseAllModal();
        SetModalActive(groupInteractCageUI, true);
    }
    #endregion

    #region OPEN MODAL
    public void OpenViewDetailAnimal(ConfigCage configCage)
    {
       
        CloseAllModal(true);
        if (configCage != null) groupDetailPanelAnimal.GetComponent<DetailPanelAnimal>().configCage = configCage;
        SetModalActive(groupDetailPanelAnimal, true);
    }
    public void OpenViewAnimal()
    {
        CloseAllModal(true); //accept close overlay UI
        SetModalActive(groupViewAnimalsUI, true);
    }
    public void OpenUnClockCage()
    {
        CloseAllModal();
        SetModalActive(GroupUnClockCage, true);
    }
    public void OpenInteractCage()
    {
        CloseAllModal();
        SetModalActive(groupInteractCageUI, true);
    }
    public void OpenOverlay()
    {
        CloseAllModal();
        SetModalActive(groupOverlayUI, true);
    }
    #endregion
    #region ANIMATION

    public void test()
    {
        StartCoroutine(ToggleFood());
    }
    public IEnumerator ToggleFood()//use in button food in interact cage 
    {
        foreach (GameObject btn in btnFoods)
        {
            Common.PopUpButton(btn, close: toggleBtnFoods);
            yield return new WaitForSeconds(0.2f);
        }
        
        toggleBtnFoods = !toggleBtnFoods;
    }
    public void ToggleMiniMap(GameObject obj) //set this event in button turn off minimap
    {
        // Toggle the MiniMap visibility and animate its position
        toggleBtnMiniMap = !toggleBtnMiniMap;

        // Use ternary operator for concise animation call
        Common.MoveObjectUI(obj, 0.4f, toggleBtnMiniMap ? 185f : -185f);
    }

    #endregion 

    public void CloseAllModal(bool Overlay=false)
    {
        groupViewAnimalsUI.SetActive(false);
        groupInteractCageUI.SetActive(false);
        GroupUnClockCage.SetActive(false);
        groupDetailPanelAnimal.SetActive(false);
        groupOverlayUI.SetActive(!Overlay);    
    }

}
