using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager Instance;
    #region Timing Game
    public float maxTime = 0;
    public float currenTime = 0;
    #endregion
    #region Toggle
    public bool CanPlay;//true when over time
    #endregion
    public SOGame DataGame;
    public Animator animatorLoading;
    public GameObject warningTimer;


    


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        //--init data--
        currenTime = DataGame.CDTimerPlay;
        CanPlay = DataGame.CanPlay;
    }
    private void Start()
    {
       
        Debug.Log(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        if (currenTime > 0)
        {
            currenTime -= Time.deltaTime;
            if (currenTime <= 0)
            {
                currenTime = 0;
                CanPlay = false;
                if (!DataGame.CanPlay && SceneManager.GetActiveScene().name != "MainMenu") ActiveWarningTimerPlay(true);
                else ActiveWarningTimerPlay(false);
            }
        }
    }

    private void OnDestroy()
    {
        DataGame.CDTimerPlay = currenTime;
        DataGame.CanPlay = CanPlay;
    }
 

    [ProButton]
    public void LoadingCanvas()
    {
        Debug.Log("Trigger loading");
        animatorLoading.SetTrigger("trigger");
        StartCoroutine(Common.delayCoroutine(2f, () =>
        {
            animatorLoading.SetTrigger("trigger");
        }));
    }
    public void ActiveWarningTimerPlay(bool status)
    {
        warningTimer.SetActive(status);
    }
    public void LoadSceneMainMenu()
    {
        StartCoroutine(Common.LoadSceneAsync(GameScenes.MainMenu));
        if (!DataGame.CanPlay && SceneManager.GetActiveScene().name != "MainMenu") ActiveWarningTimerPlay(true);
        else ActiveWarningTimerPlay(false);
    }
}
