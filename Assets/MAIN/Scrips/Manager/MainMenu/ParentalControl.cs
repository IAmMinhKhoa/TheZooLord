using System;
using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ParentalControl : MonoBehaviour
{
    [SerializeField] int a;
    [SerializeField] int b;
    [SerializeField] int answer;
    [SerializeField] List<Button> btnAnswer = new List<Button>();
    [SerializeField] GameObject mainFrame;
    //----UI ELEMENT----
    [SerializeField] TMP_Text txtA;
    [SerializeField] TMP_Text txtB;
    [SerializeField] TMP_Text txtAnwser;
    //---- VARIABLE ---
    int limitCount = 0;
    //---- ACTIONE---
    public Action afterCorrect;
    private void Start()
    {
        for (int i = 0; i < btnAnswer.Count; i++)
        {
            int temp = i + 1;
            btnAnswer[i].onClick.AddListener(() =>
            {
                checkAnswer(temp);
            });
        }
    }
   
    public void Init()
    {
        do
        {
            a = UnityEngine.Random.Range(1, 5);
            b = UnityEngine.Random.Range(1, 5);
        } while (a + b >= 10);

        answer = a + b;
        //set in UI
        txtA.text = a.ToString();
        txtB.text = b.ToString();
    }
    public void checkAnswer(int key)
    {
        StartCoroutine(IEcheckAnswer(key)); 
    }
    private IEnumerator IEcheckAnswer(int key)
    {
        Debug.Log(key);
        txtAnwser.text =key.ToString();
        if(answer==key)
        {
            SoundManager.instance.PlaySound(SoundType.Success);
            ClosePanel();
            afterCorrect?.Invoke();

            yield return null;
        }
       

        limitCount++;
        if (limitCount < 3)
        {
            SoundManager.instance.PlaySound(SoundType.Failed);
            yield return Common.ShakeObject(mainFrame);
        }
        else ClosePanel();

        yield return null;

    }



    #region OPEN/CLOSE
    //default y=-1350 (below screen)
    //when open y=-30(center screen)
    [ProButton]
    public void OpenPanel()
    {
        Init();
        Common.MoveObjectUI(mainFrame, 0.25f, -30f, TypeAnimationMove.vertical);
        this.gameObject.SetActive(true);
    }
    [ProButton]
    public void ClosePanel()
    {
       
       limitCount = 0;
        this.gameObject.SetActive(false);
        Common.MoveObjectUI(mainFrame, 0.1f, -1350f, TypeAnimationMove.vertical);


    }
    #endregion
}
