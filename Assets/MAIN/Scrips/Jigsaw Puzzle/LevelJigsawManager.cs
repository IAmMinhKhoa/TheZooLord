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

    [SerializeField] SpriteRenderer completeImage;
    [SerializeField] SpriteRenderer hintImage;

    [SerializeField] GameObject parentPieces;
    [SerializeField] List<GameObject> listPuzzlePieces;

    PuzzleManager puzzleManager;

    private void Awake()
    {
        ButtonsArray();
        GetPieceFromParent();
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
    }

    void ButtonsArray()
    {
        buttons = levelButtons.GetComponentsInChildren<Transform>(true)
                .Where(obj => obj.CompareTag("Level"))
                .Select(obj => obj.gameObject.GetComponent<Button>())
                .ToArray();
    }

    void GetPieceFromParent()
    {
        listPuzzlePieces = new List<GameObject>();

        for (int i = 0; i < parentPieces.transform.childCount; i++)
        {
            GameObject child = parentPieces.transform.GetChild(i).gameObject;
            GameObject piece = child.transform.Find("Piece").gameObject;
            listPuzzlePieces.Add(piece);
        }
    }
    public void SetPuzzlePhoto(Image photo)
    {
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        UnlockNewLevel(buttonName);
        for (int i = 0; i < listPuzzlePieces.Count; i++)
        {
            listPuzzlePieces[i].GetComponent<SpriteRenderer>().sprite = photo.sprite;
            completeImage.sprite = photo.sprite;
            hintImage.sprite = photo.sprite;
        }
        parentPieces.transform.parent.gameObject.SetActive(true);
        levelCanvas.gameObject.SetActive(false);
    }

    void UnlockNewLevel(string levelButton)
    {
        string[] levelNumberString = levelButton.Split(' ');
        int levelNumber = int.Parse(levelNumberString[1]) - 1;
        if ((levelNumber) >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            Debug.Log(levelNumber);
            Debug.Log(PlayerPrefs.GetInt("ReachedIndex"));
            PlayerPrefs.SetInt("ReachedIndex", levelNumber + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();

        }
    }
}
