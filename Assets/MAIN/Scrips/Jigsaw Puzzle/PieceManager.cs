using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    [SerializeField] GameObject parentPieces;
    [SerializeField] GameObject parentTarget;

    [SerializeField] List<GameObject> listPuzzlePieces;
    [SerializeField] List<GameObject> listTarget;

    [SerializeField] GameObject hintCanvas;
    [SerializeField] GameObject CompleteImage;

    ScoreKeeper scoreKeeper;

    private void Awake()
    {
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
        CompleteImage.SetActive(false);
        hintCanvas.SetActive(true);
        GetPieceFromParent();
        GetTargetFromParent();
        ShuffleGameObjectList<GameObject>(listPuzzlePieces);
        ChangePosPiece();
    }

    private void OnDisable()
    {
        ResetPosPiece();
    }
    // Update is called once per frame
    void Update()
    {
        if (scoreKeeper.GetCorrectPieces() == listPuzzlePieces.Count)
        {
            JigsawGameManager.instance.PlayClapWin();
            JigsawGameManager.instance.UnlockNewLevel();
            CompleteImage.SetActive(true);
            hintCanvas.SetActive(false);
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
            listPuzzlePieces[i].transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

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

    void PlaySoundWin()
    {

    }
}
