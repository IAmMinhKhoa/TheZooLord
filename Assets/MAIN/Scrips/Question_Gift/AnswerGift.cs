using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerGift : MonoBehaviour
{
    #region UI ELEMENT OF BUTTON ANSWER
    public Image img_Answer;
    public Image img_Result;
    #endregion
    protected bool ExactlyAnswer;
    public Sprite IconExactly;
    public Sprite IconNotExactly;
    protected Action ActionExactly;
    protected Action ActionNotExactly;
    private void Start()
    {
        img_Result.enabled = false;
    }

    public void Init(Sprite img,bool rightAnswer,Action Right,Action NotRight)
    {
        img_Answer.sprite = img;
        ExactlyAnswer = rightAnswer;
        ActionExactly = Right;
        ActionNotExactly = NotRight;
    }
    public void ShowResult()
    {
        if (ExactlyAnswer)
        {
            img_Result.enabled = true;
            img_Result.sprite = IconExactly;
            ActionExactly?.Invoke();
        }
        else
        {
            img_Result.enabled = true;
            img_Result.sprite = IconNotExactly;
            ActionNotExactly?.Invoke();
        }

    }
}
