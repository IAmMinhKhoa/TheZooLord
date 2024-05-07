using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MainMenu_CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera currentCamera;
    [SerializeField] RectTransform panelMainMenu;
    [SerializeField] GameObject cutScenceTimeLine;
    [SerializeField] float topPosY, middlePosY;
    [SerializeField] float tweenDuration;

    public Ease easeInType;
    public Ease easeOutType;


    private void Start()
    {
        currentCamera.Priority++;
        cutScenceTimeLine.SetActive(false);
    }

    public void BackMainMenu(CinemachineVirtualCamera target)
    {
        currentCamera.Priority--;

        currentCamera = target;

        currentCamera.Priority++;
        panelMainMenu.DOAnchorPosY(middlePosY, tweenDuration).SetEase(easeInType);

    }

    public async void ChangeChooseZooCamera(CinemachineVirtualCamera target)
    {
        await panelMainMenu.DOAnchorPosY(topPosY, tweenDuration).SetEase(easeOutType).AsyncWaitForCompletion();
        cutScenceTimeLine.SetActive(true);
        currentCamera.Priority--;

        currentCamera = target;
        StartCoroutine(FinishCut());
    }

    public void ChangeMinigameCamera(CinemachineVirtualCamera target)
    {
        panelMainMenu.DOAnchorPosY(topPosY, tweenDuration);

        currentCamera.Priority--;

        currentCamera = target;

        currentCamera.Priority++;
    }

    IEnumerator FinishCut()
    {
        yield return new WaitForSeconds(6f);
        currentCamera.Priority++;
    }
}
