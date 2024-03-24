using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleSelect : MonoBehaviour
{
    [SerializeField] GameObject startPanel;
    public void SetPuzzlePhoto(Image photo)
    {
        GameObject[] pieces;
        for (int i = 1; i <= 36 ; i++)
        {
            GameObject.Find("Piece (" + i + ")").transform.Find("Puzzle").GetComponent<SpriteRenderer>().sprite = photo.sprite;
        }
        startPanel.SetActive(false);
    }
}
