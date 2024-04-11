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
        pauseMenu.SetActive(false);
        levelMenu.SetActive(true);
    }

    public void OnAgainButton()
    {
        pauseMenu.SetActive(false);
        Game.Instance.StartNext(Game.Instance.levelCurrent);
    }

    void PausePanelIntro()
    {
        canvasGroup.DOFade(1, tweenDuration);
        pausePanelRect.DOAnchorPosY(middlePosY, tweenDuration);
        pauseButtonRect.DOAnchorPosX(80, tweenDuration);

    }

    async Task PausePanelOutro()
    {
        canvasGroup.DOFade(0, tweenDuration);
        await pausePanelRect.DOAnchorPosY(topPosY, tweenDuration).AsyncWaitForCompletion();
        pauseButtonRect.DOAnchorPosX(-80, tweenDuration);
    }
}
