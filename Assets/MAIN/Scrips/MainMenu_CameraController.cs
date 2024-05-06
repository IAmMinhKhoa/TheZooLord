using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_CameraController : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject chooseZooCamera;

    public void SetMainCam()
    {
        mainCamera.SetActive(true);
        chooseZooCamera.SetActive(false);
    }

    public void SetChooseZooCam()
    {
        mainCamera.SetActive(false);
        chooseZooCamera.SetActive(true);
    }
}
