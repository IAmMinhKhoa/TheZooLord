using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    public static AnimalManager Instance { get; private set; }

    [SerializeField] GameObject parentPieces;
    [SerializeField] GameObject parentTarget;

    [SerializeField] List<GameObject> listPuzzlePieces;
    [SerializeField] List<GameObject> listTarget;

    [SerializeField] GameObject completeObject;

    ScoreKeeper scoreKeeper;

    public bool isComplete = false;

    private void Awake()
    {
        Instance = this;
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        completeObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        listPuzzlePieces = new List<GameObject>();
        listTarget = new List<GameObject>();
        GetPieceFromParent();
        GetTargetFromParent();


        //ShuffleGameObjectList(listPuzzlePieces);
        ChangePosPiece();
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreKeeper.GetCorrectPieces() == listPuzzlePieces.Count && !isComplete)
        {
            isComplete = true;
            PuzzleManager.instance.PlayClapWin();
            completeObject.SetActive(true);
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
            listPuzzlePieces[i].transform.position = listTarget[i].transform.position;
            listPuzzlePieces[i].GetComponent<Puzzle>().initialPosition = (Vector3)listTarget[i].transform.position;
        }    
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
