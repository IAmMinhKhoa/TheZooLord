using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MainMenu_CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Animator animPlayer;

    Vector3 startPos;


    [SerializeField] CinemachineVirtualCamera currentCamera;
    [SerializeField] RectTransform panelMainMenu;
    [SerializeField] GameObject cutScenceChooseZooTimeLine;
    [SerializeField] GameObject cutScenceMiniGameTimeLine;

    [SerializeField] float topPosY, middlePosY;
    [SerializeField] float tweenDuration;

    public Ease easeInType;
    public Ease easeOutType;

    bool activeCutScenceZoo = false;
    bool activeCutScenceMinigame = false;


    private void Start()
    {
        startPos = player.transform.position;
        animPlayer = player.GetComponent<Animator>();
        currentCamera.Priority++;
        cutScenceChooseZooTimeLine.SetActive(false);
        cutScenceMiniGameTimeLine.SetActive(false);
    }

    public void BackMainMenu(CinemachineVirtualCamera target)
    {
        player.SetActive(true);

        player.transform.position = startPos;
        player.transform.rotation = Quaternion.Euler(0, -90, 0);

        currentCamera.Priority--;

        currentCamera = target;

        currentCamera.Priority++;
        panelMainMenu.DOAnchorPosY(middlePosY, tweenDuration).SetEase(easeInType);

    }

    public async void ChangeChooseZooCamera(CinemachineVirtualCamera target)
    {
        ActiveAnimationChangeCam();
        await panelMainMenu.DOAnchorPosY(topPosY, tweenDuration).SetEase(easeOutType).AsyncWaitForCompletion();
        currentCamera.Priority--;

        currentCamera = target;

        if (!activeCutScenceZoo)
        {
            activeCutScenceZoo = true;
            cutScenceChooseZooTimeLine.SetActive(true);
        }
        StartCoroutine(FinishZooCut());
        currentCamera.Priority++;
    }

    public async void ChangeMinigameCamera(CinemachineVirtualCamera target)
    {
        ActiveAnimationChangeCam();
        await panelMainMenu.DOAnchorPosY(topPosY, tweenDuration).SetEase(easeOutType).AsyncWaitForCompletion();

        currentCamera.Priority--;

        currentCamera = target;

        if (!activeCutScenceMinigame)
        {
            activeCutScenceMinigame = true;
            cutScenceMiniGameTimeLine.SetActive(true);
        }
        StartCoroutine(FinishMinigameCut());
        currentCamera.Priority++;
    }

    private void ActiveAnimationChangeCam()
    {
        animPlayer.SetInteger("DanceIndex", Random.Range(0, 4));
        animPlayer.SetTrigger("Dance");
    }

    IEnumerator FinishZooCut()
    {
        yield return new WaitForSeconds(9f);
        player.SetActive(false);
    }

    IEnumerator FinishMinigameCut()
    {
        yield return new WaitForSeconds(19f);
        player.SetActive(false);
    }
}
