
using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    #region ANIMATION DOTWEEN
    public static void PopUpButton(GameObject obj, float scale = 1, float duration = 0.5f, float delay = 0, bool close = false)
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
    /// <summary>
    /// USE FOR UI ELEMTENT
    /// </summary>
    /// <param name="objectToMove"></param>
    /// <param name="duration"></param>
    /// <param name="endPos"></param>
    /// <param name="type"></param>
    /// <param name="ease"></param>
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
    /// <summary>
    ///  USE FOR UI ELEMTENT
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="duration"></param>
    /// <param name="strength"></param>
    /// <param name="vibrato"></param>
    /// <param name="randomness"></param>
    /// <param name=""></param>
    public static IEnumerator ShakeObject(GameObject obj, 
         float duration = 1f,
         float strength = 10f,
         int vibrato = 20,
         float randomness = 180f)
    {
        yield return obj.GetComponent<RectTransform>().DOShakePosition(duration, strength, vibrato, randomness).WaitForCompletion();
    }
    #endregion


    public static void LoadScene(GameScenes scene)
    {
        //do something before load next scenes
        SceneManager.LoadScene(scene.ToString());
    }

}
