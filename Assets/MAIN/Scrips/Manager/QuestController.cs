using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class QuestController : MonoBehaviour
{

    #region singleton
    public static QuestController Instance
    {
        get;
        private set;
    }
    #endregion

    [SerializeField] protected CanvasGroup canvasGroup;

    #region UI ELEMENT OF QUESTION
    public TMP_Text textScrip;
    public Button btnSound;
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
    #region FX
    public GameObject correctFX;
    public GameObject wrongFX;
    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
       
    }

    private void Start()
    {  
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    
    }
    
    public IEnumerator GenerateQuest(SOQuestion soQuest=null)
    {
        SOQuestion currentSoQuest;
        if (soQuest != null) currentSoQuest = soQuest;
        else currentSoQuest= RandomQuestion();
        SetDataToUi(currentSoQuest);
        foreach (var data in currentSoQuest.dataAnswers)
        {
            GameObject objAnswer = Instantiate(buttonAnswerPrefab, parentAnswer.transform);
            //-> animation <-
            objAnswer.transform.DOScale(Vector3.one, 0.5f);
            yield return new WaitForSeconds(0.2f);

            CurrentObjAnswers.Add(objAnswer);

            AnswerGift answerGift = objAnswer.GetComponent<AnswerGift>();
            answerGift.Init(data.img, data.rightAnswer, ActionSuccessAnswer, ActionFailedAnswer);

        }

    }
    protected void ActionSuccessAnswer()
    {
        correctFX.SetActive(true);
        ActiveEmojiQuest(1);
        LockButtonAnswer();
        //need sound FX success
        StartCoroutine(Common.delayCoroutine(3f, () =>
        {
            CloseModal();
        }));
        affterSuccess?.Invoke();
    }
    protected void ActionFailedAnswer()
    {
        wrongFX.SetActive(true);
        ActiveEmojiQuest(2);
        LockButtonAnswer();
        //need sound FX fail
        StartCoroutine(Common.delayCoroutine(3f, () =>
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
        btnSound.onClick.AddListener(() => { SoundManager.instance.PlayAudioSingle(data.voiceQuest); });
        float delay = 0.5f;
        for (int i = 0; i < data.typeDiff; i++) startDifficults[i].color = new Color(250, 255, 0, 255);

        for (int i = 0; i < startDifficults.Count; i++)
        {

            startDifficults[i].transform.localScale = Vector3.zero; // ??t scale ban ??u là 0

            // S? d?ng DOTween ?? scale t? giá tr? ban ??u ??n giá tr? mong mu?n trong kho?ng th?i gian nh?t ??nh
            startDifficults[i].transform.DOScale(Vector3.one, 0.5f).SetDelay(delay);
            delay += 0.5f;
        }
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
        //reset Action affter answer question
        affterFail=null;
        affterSuccess = null;
        //turn off all FX
        correctFX.SetActive(false);
        wrongFX.SetActive(false);
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

    public  void OpenModal(SOQuestion soQuest=null,Action affterSuccess=null,Action affterFailed=null)
    {
        this.affterSuccess= affterSuccess;
        this.affterFail = affterFailed;
        canvasGroup.DOFade(1f, 0.4f).OnStart(() =>
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        });
        StartCoroutine(GenerateQuest(soQuest));
        //Default start question -> emoji default
        ActiveEmojiQuest(0);

    }
    public void CloseModal()
    {
        canvasGroup.DOFade(0f, 0.3f).OnComplete(() =>
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        });
        ClearUI();
    }

 
    #endregion
}
