using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    public static PieceManager Instance { get; private set; }

    [SerializeField] GameObject parentPieces;
    [SerializeField] GameObject parentTarget;

    [SerializeField] List<GameObject> listPuzzlePieces;
    [SerializeField] List<GameObject> listTarget;

    [SerializeField] GameObject completeObject;

    [SerializeField] GameObject hintCanvas;
    [SerializeField] GameObject CompleteImage;

    public bool isComplete = false;   //cần sửa khi thắng

    ScoreKeeper scoreKeeper;

    private void Awake()
    {
        Instance = this;

        listPuzzlePieces = new List<GameObject>();
        listTarget = new List<GameObject>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    //private void OnEnable()
    //{
    //    //isComplete = false;
    //    completeObject.SetActive(false);
    //    GetPieceFromParent();
    //    GetTargetFromParent();
    //    ChangePosPiece();

    //}

    private void Start()
    {
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void OnEnable ()
    {
        isComplete = false;
        completeObject.SetActive(false);
        hintCanvas.SetActive(false);
        completeObject.SetActive(false);
        GetPieceFromParent();
        GetTargetFromParent();
        ShuffleGameObjectList<GameObject>(listPuzzlePieces);
        ChangePosPiece();
    }

    private void OnDisable()
    {
        isComplete = false;
        ResetPosPiece();
    }
    // Update is called once per frame
    void Update()
    {
        if (scoreKeeper.GetCorrectPieces() == listPuzzlePieces.Count && !isComplete)
        {
            isComplete = true;
            JigsawGameManager.instance.PlayClapWin();
            completeObject.SetActive(true);
            hintCanvas.SetActive(false);
            //UnlockNewLevel();
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
        for (int i = 0; i < listTarget.Count; i++)
        {
            listPuzzlePieces[i].GetComponent<Piece>().rightPosition = listPuzzlePieces[i].transform.localPosition;
            listPuzzlePieces[i].transform.localPosition = listTarget[i].transform.localPosition;
            listPuzzlePieces[i].GetComponent<Piece>().initialPosition = (Vector3)listTarget[i].transform.localPosition;
        }
    }

    void ResetPosPiece()
    {
        for (int i = 0; i < listPuzzlePieces.Count; i++)
        {
            listPuzzlePieces[i].transform.localPosition = listPuzzlePieces[i].GetComponent<Piece>().rightPosition;
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

    //void UnlockNewLevel()
    //{
    //    string[] levelNumberString = gameObject.name.Split(' ');
    //    int levelNumber = int.Parse(levelNumberString[1]) - 1;
    //    if ((levelNumber) >= PlayerPrefs.GetInt("ReachedIndex"))
    //    {
    //        Debug.Log(levelNumber);
    //        Debug.Log(PlayerPrefs.GetInt("ReachedIndex"));
    //        PlayerPrefs.SetInt("ReachedIndex", levelNumber + 1);
    //        PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
    //        PlayerPrefs.Save();

    //    }
    //}

    void PlaySoundWin()
    {

    }
}
