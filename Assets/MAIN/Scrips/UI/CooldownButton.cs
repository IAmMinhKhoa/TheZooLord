using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CooldownButton : MonoBehaviour
{
    int countClick = 0;
    public event Action actionAfterCD;
    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(Select);    
    }
    private void Select()
    {
        countClick++;
        this.transform.DOScale(1.6f, 0.5f);
        if (countClick >= 2)
        {
            //do something
            actionAfterCD?.Invoke();
            this.transform.DOScale(1f, 0.2f);
            return;
        }
        StartCoroutine(Common.delayCoroutine(2f, () =>
        {
            countClick = 0;
            this.transform.DOScale(1f, 0.2f);
        }));
    }
}
