using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private void Update()
    {
        if (ScoreKeeper.instance.GetCorrectPieces() != 0)
        {
            Destroy(gameObject);
        }
    }
}
