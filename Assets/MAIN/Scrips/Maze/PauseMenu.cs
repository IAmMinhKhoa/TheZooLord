using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] RectTransform pausePanelRect, pauseButtonRect;
    [SerializeField] float topPosY, middlePosY;
    [SerializeField] float tweenDuration;
    [SerializeField] CanvasGroup canvasGroup;

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    public void OnPauseButtonn()
    {
        pauseMenu.SetActive(pauseMenu);
        PausePanelIntro();
    }

    public async void OnPlayButton()
    {
        await PausePanelOutro();
        pauseMenu.SetActive(false); 
    }

    public void OnBackButton(GameObject levelMenu)
    {
        canvasGroup.DOFade(0, tweenDuration);
        pausePanelRect.DOAnchorPosY(topPosY, tweenDuration);
        pauseButtonRect.DOAnchorPosX(-185, tweenDuration);
        pauseMenu.SetActive(false);
        levelMenu.SetActive(true);
    }

    public async void OnAgainButton()
    {
        await PausePanelOutro();
        pauseMenu.SetActive(false);
        Game.Instance.StartNext(Game.Instance.levelCurrent);
    }

    void PausePanelIntro()
    {
        canvasGroup.DOFade(1, tweenDuration);
        pausePanelRect.DOAnchorPosY(middlePosY, tweenDuration).SetEase(Ease.OutElastic);
        pauseButtonRect.DOAnchorPosX(185, tweenDuration).SetEase(Ease.InQuint);

    }

    async Task PausePanelOutro()
    {
        canvasGroup.DOFade(0, tweenDuration);
        await pausePanelRect.DOAnchorPosY(topPosY, tweenDuration).SetEase(Ease.InQuint).AsyncWaitForCompletion();
        pauseButtonRect.DOAnchorPosX(-185, tweenDuration);
    }
}
