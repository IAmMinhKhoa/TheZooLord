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
    public void GoToSceneMainMenu()
    {
        Common.LoadScene(GameScenes.MainMenu);
    }
    public void GoToSceneMap1()
    {
        Common.LoadScene(GameScenes.BuildMap);
    }

    //Minigame
    public void GoToSceneAnimalPuzzle()
    {
        Common.LoadScene(GameScenes.AnimalPuzzle);
    }
    public void GoToSceneJigsawPuzzle()
    {
        Common.LoadScene(GameScenes.JigsawPuzzle);
    }
    public void GoToSceneMaze()
    {
        Common.LoadScene(GameScenes.Maze);
    }
    #endregion
}
