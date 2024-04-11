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
    [Header("MAIN UI CHARACTER")]
    public GameObject groupDetailPanelAnimal;
    public GameObject groupViewAnimalsUI;
    public GameObject groupInteractCageUI;
    public GameObject groupOverlayUI;

    [Header("LIST UI")]
    public List<GameObject> btnFoods;

   //--- USE IN LOCAL ---
    private bool toggleBtnFoods=false;

    private void Start()
    {
        this.Register(EventID.OpenUiOverlay, OpenModalUiOverlay);
    }

    private void OnDestroy()
    {
        this.Unregister(EventID.CloseUiOverlay, CloseModalUiOverlay);
    }
    public void OpenModalUiOverlay(object data)
    {
        SetModalActive(groupOverlayUI, true);
    }
    public void CloseModalUiOverlay(object data=null)
    {
        SetModalActive(groupOverlayUI, false);
    }
    public void OpenModalInteract()
    {
        SetModalActive(groupInteractCageUI, true);
    }
    public void CloseModalInteract()
    {
        SetModalActive(groupInteractCageUI, false);
    }


    public void CloseModalViewAnimals()
    {
        CloseAllModal();
        SetModalActive(groupOverlayUI, true);
        SetModalActive(groupInteractCageUI, true);
    }

    public void OpenModalViewAnimals()
    {
        CloseAllModal();
        SetModalActive(groupViewAnimalsUI, true);
    }

    public void OpenModalOverView()
    {
        CloseAllModal();
        SetModalActive(groupOverlayUI, true);
    }
    public void OpenModalDetailPanel()
    {
        CloseAllModal();
        SetModalActive(groupDetailPanelAnimal, true);
    }
    protected void SetModalActive(GameObject obj,bool boolean)
    {
        obj.SetActive(boolean);
    }
    public void CloseAllModal()
    {
        groupViewAnimalsUI.SetActive(false);
        groupInteractCageUI.SetActive(false);
        groupOverlayUI.SetActive(false);
    }

    public void animationButtonFood()
    {
        foreach (GameObject btn in btnFoods)
        {
            Common.PopUpButton(btn,close: toggleBtnFoods);
        }
        toggleBtnFoods = !toggleBtnFoods;
    }

    #region TESTING
    [ProButton]
    protected void OpenButtonFood()
    {
        animationButtonFood();
    }
    [ProButton]
    protected void CloseButtonFood()
    {
        animationButtonFood();
    }
    #endregion

}
