using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager Instance;
    #region Timing Game
    public float maxTime = 0;
    public float currenTime = 0;
    #endregion
    #region Toggle
    public bool togglePause;
    #endregion
    public SOGame DataGame;
    public Animator animatorLoading;



    


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        //--init data--
        currenTime = maxTime;
    }

    private void Update()
    {
        if (currenTime > 0)
        {
            currenTime -= Time.deltaTime;
            if (currenTime <= 0)
            {
                currenTime = 0;
                TogglePauseGame(true);
            }
        }
    }

    public void TogglePauseGame(bool force=false)
    {
        if (force)
        {
            Time.timeScale = 0;
            togglePause = !togglePause;
            return;
        }
        if(togglePause) Time.timeScale = 0;
        else Time.timeScale = 1;
        togglePause = !togglePause;
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
}
