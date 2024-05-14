using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] Button[] buttons;
    public GameObject levelPages;

    [SerializeField] Canvas levelCanvas;
    int unlockedLevel;

    PuzzleManager puzzleManager;

    public int levelActive;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        puzzleManager = FindObjectOfType<PuzzleManager>();
        ButtonsArray();
        unlockedLevel = PlayerPrefs.GetInt("UnlockedPuzzleLevel", 1);
    }

    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("UnlockedPuzzleLevel", 1) <= buttons.Length)
        {
            unlockedLevel = PlayerPrefs.GetInt("UnlockedPuzzleLevel", 1);
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
    public void OpenLevel(Button btn)
    {
        string[] btnName = btn.transform.name.Split(' ');
        int levelId = int.Parse(btnName[1]);
        levelCanvas.gameObject.SetActive(false);
        puzzleManager.SetActiveLevel(levelId);
        LevelController.Instance.levelActive = levelId; 
    }

    void ButtonsArray()
    {
        buttons = levelPages.GetComponentsInChildren<Transform>(true)
                .Where(obj => obj.CompareTag("Level"))
                .Select(obj => obj.gameObject.GetComponent<Button>())
                .ToArray();
    }
}
