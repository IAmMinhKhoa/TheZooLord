using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(instance);
        }
    }
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

    public void ResetCorrectPieces()
    {
        correctPieces = 0;
    }
}
