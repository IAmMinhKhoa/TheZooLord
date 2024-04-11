using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GiftController : MonoBehaviour,IPointerClickHandler
{
    public GameObject mainCanvas;
    public List<SOQuestion> SoQuests;

    #region UI ELEMENT OF QUESTION
    public TMP_Text textScrip;
    public AudioSource audioSouce;
    public GameObject parentAnswer;
    public GameObject buttonAnswerPrefab;
    public List<Image> startDifficults;
    #endregion
    #region 3 TYPE OF QUESTION
    public List<GameObject> emojiQuests; //0: default , 1: rightAnswer, 2: NotAxactly
    #endregion
    private List<GameObject> CurrentObjAnswers =new List<GameObject>();
    private SOQuestion currentSoQuest;

    protected CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = mainCanvas.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;


        
    }

    public void GenerateQuest()
    {
        currentSoQuest = RandomQuestion();
        SetDataToUi(currentSoQuest);
        foreach (var data in currentSoQuest.dataAnswers)
        {
            GameObject objAnswer = Instantiate(buttonAnswerPrefab, parentAnswer.transform);
            CurrentObjAnswers.Add(objAnswer);

            AnswerGift answerGift = objAnswer.GetComponent<AnswerGift>();
            answerGift.Init(data.img, data.rightAnswer, ActionRightAnswer, ActionNotRightAnswer);
           
        }

    }
    protected void ActionRightAnswer()
    {
        ActiveEmojiQuest(1);
        StartCoroutine(Common.delayCoroutine(2f, () =>
        {
            CloseModal();

        }));
    }
    protected void ActionNotRightAnswer()
    {
        ActiveEmojiQuest(2);
        StartCoroutine(Common.delayCoroutine(2f, () =>
        {
            CloseModal();

        }));
    }
    private void OnButtonClick(bool rightAnswer)
    {
        
        if (rightAnswer)
        {
            Debug.Log(rightAnswer);
         
            for (int i = 0; i < currentSoQuest.dataAnswers.Count; i++)
            {
                if (!currentSoQuest.dataAnswers[i].rightAnswer)
                {
                    CurrentObjAnswers[i].GetComponent<Button>().interactable = false;
                }
            }
            ActiveEmojiQuest(1);
            StartCoroutine(Common.delayCoroutine(2f, () =>
            {
                CloseModal();
    
            }));
        }
        else
        {

            ActiveEmojiQuest(2);
            StartCoroutine(Common.delayCoroutine(2f, () =>
            {
                CloseModal();

            }));
        }
    }
    protected void SetDataToUi(SOQuestion data)
    {
       textScrip.text = data.scripQuestion;
       audioSouce.clip = data.voiceQuest;
       for(int i =0; i < data.typeDiff; i++) startDifficults[i].color = new Color(250, 255, 0, 255);
    }
    protected void ClearUI()
    {
        //reset color start difficultClearUI
        foreach (var obj in startDifficults) obj.color = Color.black;
        //destroy all old answer
        CurrentObjAnswers.Clear();
        
        for(int i=0; i< parentAnswer.transform.childCount;i++)
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
    private void ResetEmojiObject() {
        foreach (var emoji in emojiQuests) emoji.SetActive(false);
    }
    protected SOQuestion RandomQuestion()
    {
        if (SoQuests.Count == 0)
        {
            Debug.LogWarning("The list of SOQuestions is empty!");
            return null;
        }
        int randomIndex = Random.Range(0, SoQuests.Count);
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

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("egeg");
    }
    #endregion
}
