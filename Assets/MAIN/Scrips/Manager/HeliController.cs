using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HeliController : MonoBehaviour
{
   protected enum StateHeli
    {
        startMoving,
        waitting,
        endMoving

    }
    public Action endActioneHeli;
    public Transform targetPosition; //target cage

    private void Start()
    {
        // Di chuy?n GameObject t? v? trí hi?n t?i ??n v? trí targetPosition trong th?i gian duration
        transform.DOMove(targetPosition.position, 5f);
    }

}
