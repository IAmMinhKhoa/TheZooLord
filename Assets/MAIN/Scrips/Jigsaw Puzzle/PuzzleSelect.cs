using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleSelect : MonoBehaviour
{
    [SerializeField] GameObject startPanel;
    [SerializeField] SpriteRenderer completeImage;
    [SerializeField] SpriteRenderer hintImage;

    [SerializeField] GameObject parentPieces;
    [SerializeField] List<GameObject> listPuzzlePieces;

    private void Awake()
    {
        GetPieceFromParent();
    }

    private void Start()
    {
        Debug.Log(listPuzzlePieces.Count);
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
        for (int i = 0; i < listPuzzlePieces.Count ; i++)
        {
            listPuzzlePieces[i].GetComponent<SpriteRenderer>().sprite = photo.sprite;
            completeImage.sprite = photo.sprite;
            hintImage.sprite = photo.sprite;
        }
        parentPieces.transform.parent.gameObject.SetActive(true);
        startPanel.SetActive(false);
    }
}
