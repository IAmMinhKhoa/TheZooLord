using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class DataButton
{
    public Button btn;
    public Image icon;
}
public class Manager_UI : MonoBehaviour
{
    public GameObject groupViewAnimalsUI;
    public GameObject groupInteractCageUI;
    public GameObject groupOverlayUI;
    public List<DataButton> dataButtons;


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
