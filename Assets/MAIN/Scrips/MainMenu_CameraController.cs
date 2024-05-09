using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MainMenu_CameraController : MonoBehaviour
{
    public static MainMenu_CameraController Instance;
    [SerializeField] GameObject player;
    private Animator animPlayer;

    Vector3 startPos;


    public CinemachineVirtualCamera currentCamera;
    [SerializeField] CinemachineVirtualCamera mainMenuCamera;
    [SerializeField] CinemachineVirtualCamera chooseZooCamera;
    [SerializeField] CinemachineVirtualCamera minigameCamera;

    [SerializeField] RectTransform panelMainMenu;
    [SerializeField] GameObject cutScenceChooseZooTimeLine;
    [SerializeField] GameObject cutScenceMiniGameTimeLine;

    [SerializeField] float topPosY, middlePosY;
    [SerializeField] float tweenDuration;

    public Ease easeInType;
    public Ease easeOutType;

    public static bool activeCutScenceZoo = false;
    public static bool activeCutScenceMinigame = false;

    public static bool isPlayingMinigame;
    public static bool isPlayingZoo;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        startPos = player.transform.position;
        animPlayer = player.GetComponent<Animator>();
        if(isPlayingMinigame)
        {
            currentCamera = minigameCamera;
        } else if(isPlayingZoo)
        {
            currentCamera = chooseZooCamera;
        }
        else
        {
            currentCamera = mainMenuCamera;
        }
        currentCamera.Priority++;
        cutScenceChooseZooTimeLine.SetActive(false);
        cutScenceMiniGameTimeLine.SetActive(false);
    }

    private void Update()
    {

    }

    public void BackMainMenu(CinemachineVirtualCamera target)
    {
        isPlayingMinigame = false;
        isPlayingZoo = false;
        if(player.activeSelf)
        {
            player.SetActive(false);
            player.SetActive(true);
        } else
        {
            player.SetActive(true);
        }
 
        player.transform.position = startPos;
        player.transform.rotation = Quaternion.Euler(0, -90, 0);

        currentCamera.Priority--;

        currentCamera = target;

        currentCamera.Priority++;
        panelMainMenu.DOAnchorPosY(middlePosY, tweenDuration).SetEase(easeInType);

    }

    public async void ChangeChooseZooCamera()
    {
        //ActiveAnimationChangeCam();
        await panelMainMenu.DOAnchorPosY(topPosY, tweenDuration).SetEase(easeOutType).AsyncWaitForCompletion();
        currentCamera.Priority--;

        currentCamera = chooseZooCamera;

        if (!activeCutScenceZoo)
        {
            activeCutScenceZoo = true;
            cutScenceChooseZooTimeLine.SetActive(true);
            StartCoroutine(FinishZooCut());
        }
        isPlayingZoo = true;
        currentCamera.Priority++;
    }

    public async void ChangeMinigameCamera()
    {
        //ActiveAnimationChangeCam();
        await panelMainMenu.DOAnchorPosY(topPosY, tweenDuration).SetEase(easeOutType).AsyncWaitForCompletion();

        currentCamera.Priority--;

        currentCamera = minigameCamera;

        if (!activeCutScenceMinigame)
        {
            activeCutScenceMinigame = true;
            cutScenceMiniGameTimeLine.SetActive(true);
            StartCoroutine(FinishMinigameCut());
        }
        isPlayingMinigame = true;
        currentCamera.Priority++;
    }

    private void ActiveAnimationChangeCam()
    {
        animPlayer.SetInteger("DanceIndex", Random.Range(0, 4));
        animPlayer.SetTrigger("Dance");
    }

    IEnumerator FinishZooCut()
    {
        yield return new WaitForSeconds(10f);
        player.SetActive(false);
    }

    IEnumerator FinishMinigameCut()
    {
        yield return new WaitForSeconds(19f);
        player.SetActive(false);
    }
}
