using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using DG.Tweening;
using UnityEngine;

public class ButtonMove : MonoBehaviour
{
    public GameObject[] buttons;
    public float moveDuration = 1f;
    public float delayBetweenButtons = 0.5f;
    public float buttonSpacing = 100f;
    public Ease easeType = Ease.InOutQuad;

    private void OnEnable()
    {
        // Di chuy?n các button t? ph?i sang trái
        for (int i = 0; i < buttons.Length; i++)
        {
            RectTransform buttonTransform = buttons[i].GetComponent<RectTransform>();
            float targetPosX = buttonSpacing * i;

             buttonTransform.anchoredPosition = new Vector2(2000, buttonTransform.anchoredPosition.y); // ??t v? trí ban ??u c?a button

            // S? d?ng DOTween ?? di chuy?n button t? ph?i sang trái
            buttonTransform.DOAnchorPosX(targetPosX, moveDuration)
                   .SetDelay(delayBetweenButtons * i)
                   .SetEase(easeType);
        }
    }
    [ProButton]
    public void ccc()
    {
        // Di chuy?n các button t? ph?i sang trái
        for (int i = 0; i < buttons.Length; i++)
        {
            RectTransform buttonTransform = buttons[i].GetComponent<RectTransform>();
            float targetPosX = buttonSpacing * i;

            // buttonTransform.anchoredPosition = new Vector2(buttonSpacing, buttonTransform.anchoredPosition.y); // ??t v? trí ban ??u c?a button

            // S? d?ng DOTween ?? di chuy?n button t? ph?i sang trái
            buttonTransform.DOAnchorPosX(targetPosX, moveDuration)
                    .SetDelay(delayBetweenButtons * i)
                    .SetEase(easeType);
        }
    }
}
