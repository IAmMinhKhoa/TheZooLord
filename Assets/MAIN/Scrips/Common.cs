
using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Common 
{
    public static IEnumerator delayCoroutine(float delay, Action action)
    {
        yield return Delay(delay);
        action?.Invoke();
        yield return action;
    }
    public static IEnumerator Delay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    public static void PopUpButton(GameObject obj, float scale = 1, float duration = 0.5f, float delay=0,bool close=false)
    {
        if (!close)//OPEN POPUP
        {
            obj.transform.DOScale(Vector3.one * scale, duration)
               .SetEase(Ease.InBounce)
               .SetDelay(delay);
        }
        else //CLOSE
        {
            obj.transform.DOScale(Vector3.one * 0, 0.5f)
               .SetEase(Ease.InOutBack);
        }
        
    }
    public static void MoveObjectUI(GameObject objectToMove, float duration, float endPos, TypeAnimationMove type = TypeAnimationMove.horizontal, Ease ease = Ease.Linear)
    {
        // Move the object with additional effects
        try
        {
            switch (type)
            {
                case TypeAnimationMove.horizontal:
                    objectToMove.GetComponent<RectTransform>().DOAnchorPosX(endPos, duration, false).SetEase(ease);
                    break;
                case TypeAnimationMove.vertical:
                    objectToMove.GetComponent<RectTransform>().DOAnchorPosY(endPos, duration, false).SetEase(ease);
                    break;
                default:
                    break;
            }
        }
        catch (Exception)
        {
            Debug.LogError("SOMETHING WRONG IN MOVE OBJECT");
            throw;
        }
    }

}
