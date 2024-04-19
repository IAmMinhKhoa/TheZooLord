
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
               .SetEase(Ease.InBounce);
        }
        
    }
    public static void MoveObject( GameObject objectToMove, float duration, float endPos, TypeAnimationMove type = TypeAnimationMove.right)
    {
        // Move the object to the specified end position 
        switch (type)
        {
            case TypeAnimationMove.up:
                break;
            case TypeAnimationMove.down:
                
                break;
            case TypeAnimationMove.left:
                break;
            case TypeAnimationMove.right:
                objectToMove.GetComponent<RectTransform>().DOAnchorPosX(endPos, duration, false);
                break;
            default:
                break;
        }
        
    }

}
