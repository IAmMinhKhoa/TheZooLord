using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctPieces = 0;
    // Start is called before the first frame update

    public int GetCorrectPieces()
    {
        return correctPieces;
    }

    public void IncrementCorrectPieces()
    {
        correctPieces++;
    }
}
