using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class AnimalManager : MonoBehaviour
{
    [SerializeField] GameObject parentPieces;
    [SerializeField] GameObject parentTarget;

    [SerializeField] List<GameObject> listPuzzlePieces;
    [SerializeField] List<GameObject> listTarget;

    [SerializeField] GameObject completeObject;


    public bool isComplete = false;   //cần sửa khi thắng

    ScoreKeeper scoreKeeper;

    private void Awake()
    {
        listPuzzlePieces = new List<GameObject>();
        listTarget = new List<GameObject>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void OnEnable()
    {
        isComplete = false;
        completeObject.SetActive(false);
        GetPieceFromParent();
        GetTargetFromParent();
        ChangePosPiece();

    }

    private void OnDisable()
    {
        isComplete = false;
        ResetPosPiece();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreKeeper.GetCorrectPieces() == listPuzzlePieces.Count && !isComplete)
        {
            isComplete = true;
            PuzzleManager.instance.PlayClapWin();
            completeObject.SetActive(true);
            UnlockNewLevel();
            scoreKeeper.ResetCorrectPieces();  
        }
    }

    void GetPieceFromParent()
    {
        listPuzzlePieces = new List<GameObject>();

        for (int i = 0; i < parentPieces.transform.childCount; i++)
        {
            GameObject child = parentPieces.transform.GetChild(i).gameObject;
            listPuzzlePieces.Add(child);
        }
    }

    void GetTargetFromParent()
    {
        listTarget = new List<GameObject>();

        for (int i = 0; i < parentTarget.transform.childCount; i++)
        {
            GameObject child = parentTarget.transform.GetChild(i).gameObject;
            listTarget.Add(child);
        }
    }

    void ChangePosPiece()
    {
        for(int i = 0; i < listTarget.Count; i++)
        {
            listPuzzlePieces[i].GetComponent<Puzzle>().rightPosition = listPuzzlePieces[i].transform.localPosition; 
            listPuzzlePieces[i].transform.localPosition = listTarget[i].transform.localPosition;
            listPuzzlePieces[i].GetComponent<Puzzle>().initialPosition = (Vector3)listTarget[i].transform.localPosition;
        }    
    }

    void ResetPosPiece()
    {
        for (int i = 0; i < listPuzzlePieces.Count; i++)
        {
            listPuzzlePieces[i].transform.localPosition = listPuzzlePieces[i].GetComponent<Puzzle>().rightPosition;
        }
        scoreKeeper.ResetCorrectPieces();
    }

    private void ShuffleGameObjectList<GameObject>(List<GameObject> list)
    {
        
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            GameObject temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    void UnlockNewLevel()
    {
        string[] levelNumberString = gameObject.name.Split(' ');
        int levelNumber = int.Parse(levelNumberString[1]) - 1;
        if ((levelNumber) >= PlayerPrefs.GetInt("PuzzleReachedIndex"))
        {
            Debug.Log(levelNumber);
            Debug.Log(PlayerPrefs.GetInt("PuzzleReachedIndex"));
            PlayerPrefs.SetInt("PuzzleReachedIndex", levelNumber + 1);
            PlayerPrefs.SetInt("UnlockedPuzzleLevel", PlayerPrefs.GetInt("UnlockedPuzzleLevel", 1) + 1);
            PlayerPrefs.Save();

        }
    }

    void PlaySoundWin()
    {

    }
}
