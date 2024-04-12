using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelMazeManager : MonoBehaviour
{
    public static LevelMazeManager instance;
    [SerializeField] Button[] buttons;
    public GameObject levelPages;

    [SerializeField] Canvas levelCanvas;
    int unlockedLevel;

    [SerializeField] TextMeshProUGUI levelText;
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
        ButtonsArray();
        //PlayerPrefs.DeleteAll();
        unlockedLevel = PlayerPrefs.GetInt("UnlockedMazeLevel", 1);
    }

    private void OnEnable()
    {
        Debug.Log(PlayerPrefs.GetInt("UnlockedMazeLevel", 1));
        if (PlayerPrefs.GetInt("UnlockedMazeLevel", 1) <= buttons.Length)
        {
            unlockedLevel = PlayerPrefs.GetInt("UnlockedMazeLevel", 1);
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
        levelText.text = "Level " + levelId;
        levelCanvas.gameObject.SetActive(false);
        Game.Instance.ActiveMaze(levelId);
    }

    void ButtonsArray()
    {
        buttons = levelPages.GetComponentsInChildren<Transform>(true)
                .Where(obj => obj.CompareTag("Level"))
                .Select(obj => obj.gameObject.GetComponent<Button>())
                .ToArray();
    }
}
