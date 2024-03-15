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
    public GameObject groupInteractCage;
    public List<DataButton> dataButtons;

    public Button BtnObseverCage;

    public void OpenModalInteract()
    {
        groupInteractCage.SetActive(true);
    }
    public void CloseModalInteract()
    {
        groupInteractCage.SetActive(false);
    }
}
