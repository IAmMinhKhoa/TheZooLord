using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTutorial : MonoBehaviour
{
    [SerializeField] Transform movePos;

    private void Start()
    {
        StartMoving();
    }

    //private void Update()
    //{
    //    if (ScoreKeeper.instance.GetCorrectPieces() != 0)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    private void StartMoving()
    {
        transform.DOMove(movePos.position, 1.5f).SetLoops(-1, LoopType.Restart);
    }


}
