using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager Instance;
    #region Timing Game
    public float maxTime = 0;
    [SerializeField] bool limitPlay = false;
    public float currenTime = 0;
    #endregion
    #region Toggle
    [SerializeField] bool togglePause;
    #endregion

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
                limitPlay = true;
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
}
