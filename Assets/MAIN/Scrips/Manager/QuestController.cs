using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class QuestController : MonoBehaviour
{
    private GameObject mainCanvas;

    #region UI ELEMENT OF QUESTION
    public TMP_Text textScrip;
    public AudioSource audioSouce;
    public GameObject parentAnswer;
    public GameObject buttonAnswerPrefab;
    public List<Image> startDifficults;
    #endregion
    #region List
    public List<GameObject> emojiQuests; //0: default , 1: rightAnswer, 2: NotAxactly
    public List<SOQuestion> SoQuests; //Data SO question
    private List<GameObject> CurrentObjAnswers = new List<GameObject>();// Prefab Answer
    #endregion

    #region ACTION SUCCESS & FAILED ANSWER
    public event Action affterSuccess;
    public event Action affterFail;
    #endregion

    protected CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void GenerateQuest()
    {
        SOQuestion currentSoQuest = RandomQuestion();
        SetDataToUi(currentSoQuest);
        foreach (var data in currentSoQuest.dataAnswers)
        {
            GameObject objAnswer = Instantiate(buttonAnswerPrefab, parentAnswer.transform);
            CurrentObjAnswers.Add(objAnswer);

            AnswerGift answerGift = objAnswer.GetComponent<AnswerGift>();
            answerGift.Init(data.img, data.rightAnswer, ActionSuccessAnswer, ActionFailedAnswer);

        }

    }
    protected void ActionSuccessAnswer()
    {
        ActiveEmojiQuest(1);
        LockButtonAnswer();
        //need sound FX success
        StartCoroutine(Common.delayCoroutine(2f, () =>
        {
            CloseModal();
        }));
        affterSuccess?.Invoke();
    }
    protected void ActionFailedAnswer()
    {
        ActiveEmojiQuest(2);
        LockButtonAnswer();
        //need sound FX fail
        StartCoroutine(Common.delayCoroutine(2f, () =>
        {
            CloseModal();
          
        }));
        affterFail?.Invoke();
    }
    private void LockButtonAnswer()
    {
        foreach (var item in CurrentObjAnswers)
        {
            item.GetComponent<Button>().interactable = false;
        }
    }

    protected void SetDataToUi(SOQuestion data)
    {
        textScrip.text = data.scripQuestion;
        audioSouce.clip = data.voiceQuest;
        for (int i = 0; i < data.typeDiff; i++) startDifficults[i].color = new Color(250, 255, 0, 255);
    }
    protected void ClearUI()
    {
        //reset color start difficultClearUI
        foreach (var obj in startDifficults) obj.color = Color.black;
        //destroy all old answer
        CurrentObjAnswers.Clear();

        for (int i = 0; i < parentAnswer.transform.childCount; i++)
        {
            GameObject child = parentAnswer.transform.GetChild(i).gameObject;
            Destroy(child);
        }
        ResetEmojiObject();
    }

    private void ActiveEmojiQuest(int i)
    {
        ResetEmojiObject();
        emojiQuests[i].SetActive(true);
    }
    private void ResetEmojiObject()
    {
        foreach (var emoji in emojiQuests) emoji.SetActive(false);
    }
    protected SOQuestion RandomQuestion()
    {
        if (SoQuests.Count == 0)
        {
            Debug.LogWarning("The list of SOQuestions is empty!");
            return null;
        }
        int randomIndex = UnityEngine.Random.Range(0, SoQuests.Count);
        return SoQuests[randomIndex];
    }

    #region UI
    public void OpenModal()
    {
        canvasGroup.DOFade(1f, 0.2f).OnStart(() =>
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        });
        GenerateQuest();
        //Default start question -> emoji default
        ActiveEmojiQuest(0);

    }
    public void CloseModal()
    {
        canvasGroup.DOFade(0f, 0.2f).OnComplete(() =>
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        });
        ClearUI();
    }

 
    #endregion
}
