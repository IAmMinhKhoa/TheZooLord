using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using GG.Infrastructure.Utils.Swipe;
using System;
using static UnityEditor.Experimental.GraphView.GraphView;


public class SwipeController : MonoBehaviour, IEndDragHandler
{
    [SerializeField] int maxPage;
    int currentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPagesRect;

    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;
    float dragThreshould;

    public GameObject levelBar;
    [SerializeField] Image[] barImage;
    [SerializeField] Sprite barClosed, barOpen;

    [SerializeField] Button previousBtn, nextBtn;

    private void Awake()
    {

        currentPage = 1;
        targetPos = levelPagesRect.localPosition;
        dragThreshould = Screen.width / 15;
        BarImageArray();
        UpdateBar();
        UpdateArrowButton();
    }

    public void Next() {
        if(currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStep;
            MovePage();
        }    
    }

    public void Previous() { 
        if(currentPage > 1)
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();
        }
    }

    private void MovePage()
    {
        levelPagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
        UpdateBar();
        UpdateArrowButton();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshould)
        //{
        //    if (eventData.position.x > eventData.pressPosition.x)
        //    {
        //        Previous();
        //    }
        //    else
        //    {
        //        Next();
        //    }
        //}
        //else
        //{
        //    MovePage();
        //}
        UpdateArrowButton();
    }

    void UpdateBar()
    {
        foreach (var item in barImage)
        {
            item.sprite = barClosed;
        }
        barImage[currentPage - 1].sprite = barOpen;
    }

    void BarImageArray()
    {
        int childCount = levelBar.transform.childCount;
        barImage = new Image[childCount];
        for (int i = 0; i < childCount; i++)
        {
            barImage[i] = levelBar.transform.GetChild(i).gameObject.GetComponent<Image>();
        }
    }

    void UpdateArrowButton()
    {
        nextBtn.interactable = true;
        previousBtn.interactable = true;
        if(currentPage == 1)
        {
            previousBtn.interactable = false;
        } else if (currentPage == maxPage)
        {
            nextBtn.interactable = false;
        }
    }
}
