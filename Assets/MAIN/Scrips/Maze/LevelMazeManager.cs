using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelMazeManager : MonoBehaviour
{
    public static LevelMazeManager instance;
    [SerializeField] Button[] buttons;
    public GameObject levelButtons;

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
    public void OpenLevel(int sizeMaze)
    {
        levelText.text = "Level " + sizeMaze;
        levelCanvas.gameObject.SetActive(false);
        Game.Instance.ActiveMaze(sizeMaze);
    }

    void ButtonsArray()
    {
        int childCount = levelButtons.transform.childCount;
        buttons = new Button[childCount];
        for (int i = 0; i < childCount; i++)
        {
            buttons[i] = levelButtons.transform.GetChild(i).gameObject.GetComponent<Button>();
        }
    }
}
