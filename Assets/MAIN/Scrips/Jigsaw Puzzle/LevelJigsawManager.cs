using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class LevelJigsawManager : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    public GameObject levelButtons;

    [SerializeField] Canvas levelCanvas;
    int unlockedLevel;

    private void Awake()
    {
        ButtonsArray();
        unlockedLevel = PlayerPrefs.GetInt("UnlockedJigsawLevel", 1);
    }

    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("UnlockedJigsawLevel", 1) <= buttons.Length)
        {
            unlockedLevel = PlayerPrefs.GetInt("UnlockedJigsawLevel", 1);
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

    void ButtonsArray()
    {
        buttons = levelButtons.GetComponentsInChildren<Transform>(true)
                .Where(obj => obj.CompareTag("Level"))
                .Select(obj => obj.gameObject.GetComponent<Button>())
                .ToArray();
    }

    public void OpenLevel(Image photo)
    {
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        string[] levelNumberString = buttonName.Split(' ');
        int levelNumber = int.Parse(levelNumberString[1]) - 1;
        levelCanvas.gameObject.SetActive(false);
        JigsawGameManager.instance.SetPuzzlePhoto(photo, levelNumber);
    }
}
