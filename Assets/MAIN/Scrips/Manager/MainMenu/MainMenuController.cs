using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{





    #region NAVIGATION
    public void GoToSceneChooseZoo()
    {
        Common.LoadScene(GameScenes.ChooseZoo);
    }
    public void GoToSceneChooseMinigame()
    {
        Common.LoadScene(GameScenes.ChooseMinigame);
    }
    #endregion
}
