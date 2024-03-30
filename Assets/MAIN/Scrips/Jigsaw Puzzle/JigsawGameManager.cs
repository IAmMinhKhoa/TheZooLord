using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawGameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> listJigsawPieces;
    [SerializeField] GameObject winCanvas;
    [SerializeField] GameObject hintCanvas;

    [SerializeField] GameObject puzzleImage;

    [SerializeField] float minPosX, maxPosX, minPosY, maxPosY;

    JigsawPieces jigsawPieces;

    ScoreKeeper scoreKeeper;

    public bool isComplete = false;

    private void Awake()
    {
        jigsawPieces = FindObjectOfType<JigsawPieces>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject piece in listJigsawPieces)
        {
            RandomPosPiece(piece, minPosX, maxPosX, minPosY, maxPosY);
        }
        winCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreKeeper.GetCorrectPieces() == listJigsawPieces.Count)
        {
            isComplete = true;
            winCanvas.SetActive(true);
            puzzleImage.SetActive(true);
            hintCanvas.SetActive(false);
        }
    }

    void RandomPosPiece(GameObject piece, float minPosX, float maxPosX, float minPosY, float maxPosY) 
    {
        piece.transform.position = new Vector3(Random.Range(minPosX, maxPosX), Random.Range(minPosY, maxPosY));
    }
}
