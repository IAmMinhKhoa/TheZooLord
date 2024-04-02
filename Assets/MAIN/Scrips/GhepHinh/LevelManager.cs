using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    public GameObject levelButtons;

    [SerializeField] Canvas levelCanvas;
    int unlockedLevel;

    PuzzleManager puzzleManager;

    private void Awake()
    {
        puzzleManager = FindObjectOfType<PuzzleManager>();
        ButtonsArray();
        PlayerPrefs.DeleteAll();
        unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
    }

    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("UnlockedLevel", 1) <= buttons.Length)
        {
            unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
       
        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }
    public void OpenLevel(int levelId)
    {
        levelCanvas.gameObject.SetActive(false);
        puzzleManager.SetActiveLevel(levelId);
 
    }

    void ButtonsArray()
    {
        int childCount = levelButtons.transform.childCount;
        buttons = new Button[childCount];
        for (int i = 0; i < childCount; i++) {
            buttons[i] = levelButtons.transform.GetChild(i).gameObject.GetComponent<Button>();
        }
    }
}
