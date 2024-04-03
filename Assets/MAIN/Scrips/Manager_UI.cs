using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class Manager_UI : MonoBehaviour
{
    public GameObject groupDetailPanelAnimal;
    public GameObject groupViewAnimalsUI;
    public GameObject groupInteractCageUI;
    public GameObject groupOverlayUI;

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
}
