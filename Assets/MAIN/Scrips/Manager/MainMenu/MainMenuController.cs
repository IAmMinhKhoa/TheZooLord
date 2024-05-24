using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject zooMeadowMap;
    public GameObject btnUnclockMeadow;
    private void Start()
    {
        initDataButtonMapZoo();
        SoundManager.instance.PlayRandomSound_BR();
        Game_Manager.Instance.triggerOverTime();
    }
    private void initDataButtonMapZoo()
    {
        Button BtngoToMeadow = zooMeadowMap.GetComponent<Button>();
        
        if (Game_Manager.Instance.DataGame.zooMeadow.isActive==false)
        {
            BtngoToMeadow.interactable = false;
            btnUnclockMeadow.SetActive(true);
        }
        else
        {
            BtngoToMeadow.interactable = true;
            btnUnclockMeadow.SetActive(false);
        }
        

    }
    //is active = false ==>
    //interact button_main ->false
    //active button_open ->true
    //--event--
    //button_main -> loadscene
    //button_open -> show icon lock + cost -> click -> if enought coin -> open (fx)
    //->if not enought coin -> fx shake button
    public void UnClockZoo()
    {
        int costUnClock = Game_Manager.Instance.DataGame.zooMeadow.costOpen;
        if (Game_Manager.Instance.DataGame.GetCoint()>= costUnClock)
        {
            Game_Manager.Instance.DataGame.zooMeadow.isActive = true;
            initDataButtonMapZoo();
            Game_Manager.Instance.DataGame.SubtractCoin(costUnClock);
        }
        else
        {
            RectTransform buttonTransform = btnUnclockMeadow.GetComponent<RectTransform>();
            buttonTransform.DOShakePosition(1, 5, 8);
            Debug.Log("deo du de mo");
        }
    }



    #region NAVIGATION

    public void GoToSceneChooseMinigame()
    {
        
        StartCoroutine(Common.LoadSceneAsync(GameScenes.ChooseMinigame));
    }
    public void GoToSceneMainMenu()
    {
        StartCoroutine(Common.LoadSceneAsync(GameScenes.MainMenu));
    }
    public void GoToSceneMap1()
    {
        if (!Game_Manager.Instance.triggerOverTime())
        {
            Game_Manager.Instance.ActiveWarningTimerPlay(true);
            return;
        }
        StartCoroutine(Common.LoadSceneAsync(GameScenes.BuildMap));
    }

    //Minigame
    public void GoToSceneAnimalPuzzle()
    {
        if (!Game_Manager.Instance.triggerOverTime())
        {
            Game_Manager.Instance.ActiveWarningTimerPlay(true);
            return;
        }
        StartCoroutine(Common.LoadSceneAsync(GameScenes.AnimalPuzzle));
    }
    public void GoToSceneJigsawPuzzle()
    {
        if (!Game_Manager.Instance.triggerOverTime())
        {
            Game_Manager.Instance.ActiveWarningTimerPlay(true);
            return;
        }
        StartCoroutine(Common.LoadSceneAsync(GameScenes.JigsawPuzzle));
    }
    public void GoToSceneMaze()
    {
        if (!Game_Manager.Instance.triggerOverTime())
        {
            Game_Manager.Instance.ActiveWarningTimerPlay(true);
            return;
        }
        StartCoroutine(Common.LoadSceneAsync(GameScenes.Maze));
    }
    #endregion
}
