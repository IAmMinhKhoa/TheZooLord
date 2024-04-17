using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class GiftConfig : MonoBehaviour
{
    #region CONDITION OF GIFT
    [SerializeField] private float CD_Gift = 5;

    [SerializeField] private bool CanGift = true;
    #endregion

    [SerializeField] private Button Btn_Confirm;

 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           Btn_Confirm.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Btn_Confirm.gameObject.SetActive(false);
    }
   
    public void OpenGift()
    {
        QuestController.Instance.OpenModal(
        ()=> {
            AffterSuccesQuest();
        },
        ()=>
        {
            AffterFailQuest();
        });   
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
        Btn_Confirm.interactable = false;
        yield return new WaitForSeconds(initTime);
        Btn_Confirm.interactable = true;
        CanGift = true;
    }

 
}