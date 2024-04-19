using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMiniGame : MonoBehaviour
{
    public void OnBackLevelSelect(GameObject levelMenu)
    {

        levelMenu.SetActive(true);
    }

    public void OnAgainButton()
    {
        Game.Instance.StartNext(Game.Instance.levelCurrent);
    }
}
