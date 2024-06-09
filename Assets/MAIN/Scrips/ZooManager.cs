using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using com.cyborgAssets.inspectorButtonPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ZooManager : MonoBehaviour
{
    #region SINGLOTEN
    public static ZooManager Instance { get; private set; }
    #endregion
    public CooldownButton BtnOutZoo;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes if needed
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
    private void Start()
    {
        BtnOutZoo.actionAfterCD += GoToMainMenu;
        
    }
    private void OnDestroy()
    {
        BtnOutZoo.actionAfterCD -= GoToMainMenu;
    }
    public void GoToMainMenu()
    {
        //StartCoroutine(Common.LoadSceneAsync(GameScenes.MainMenu));
        Addressables.LoadSceneAsync("MainMenu");
    }

  

}
