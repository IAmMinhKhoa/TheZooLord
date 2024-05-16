using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationBook : MonoBehaviour
{
    public Image icon;
    public TMP_Text textName;
    public SOAnimal dataAnimal;
    public Action<SOAnimal> OnSelect;
    public GameObject objLock;


    public void Select()
    {
        OnSelect.Invoke(dataAnimal);
    }
}
