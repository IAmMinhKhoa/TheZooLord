using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GiftConfig : MonoBehaviour,IPointerClickHandler
{
    #region CONDITION OF GIFT
    [SerializeField] private float CD_Gift = 5;
    [SerializeField] private bool CanGift = true;
    #endregion

    public QuestController questControl;

    private void Start()
    {
        if (questControl != null) SetActionAffterQuest();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (CanGift) questControl.OpenModal();
    }
    private void SetActionAffterQuest()
    {
        questControl.affterSuccess += AffterSuccesQuest;
        questControl.affterFail += AffterFailQuest;

    }


    private void AffterFailQuest()
    {
        StartCoroutine(CdResetGift(CD_Gift));
        //do something if have
    }
    private void AffterSuccesQuest()
    {
        StartCoroutine(CdResetGift(CD_Gift));
        //do something like : add money . . .
    }
    private IEnumerator CdResetGift(float initTime)
    {
        CanGift = false;
        yield return new WaitForSeconds(initTime);
        CanGift = true;
    }

    public AudioSource GetAudioSourceGift()
    {
        return questControl.audioSouce;
    }
    private void OnDestroy()
    {
        questControl.affterSuccess -= AffterSuccesQuest;
        questControl.affterSuccess -= AffterFailQuest;
    }
}