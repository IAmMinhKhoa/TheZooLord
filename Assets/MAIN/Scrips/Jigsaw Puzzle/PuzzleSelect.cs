using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleSelect : MonoBehaviour
{
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject puzzleImage;
    public void SetPuzzlePhoto(Image photo)
    {
        for (int i = 1; i <= 9 ; i++)
        {
            GameObject.Find("Piece (" + i + ")").transform.Find("Puzzle").GetComponent<SpriteRenderer>().sprite = photo.sprite;
            puzzleImage.GetComponent<SpriteRenderer>().sprite = photo.sprite;
        }
        startPanel.SetActive(false);
    }
}
