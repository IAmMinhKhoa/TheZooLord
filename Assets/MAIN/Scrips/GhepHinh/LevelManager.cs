using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Canvas levelCanvas;
    public void OpenLevel(int levelId)
    {
        levelCanvas.gameObject.SetActive(false);    
        PuzzleManager.instance.levelPress = levelId - 1;
    }
}
