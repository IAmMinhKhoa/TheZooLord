using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public static ShakeCamera Instance;
    public NoiseSettings customNoiseProfile;
    public CinemachineFreeLook freeLookCamera;
    //public float shakeTimer = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

    }
    private void Start()
    {
        freeLookCamera = GetComponent<CinemachineFreeLook>();
        
    }
    [ProButton]
    public void Shake(int shakeTimer)
    {
        InitShakeCam(customNoiseProfile);
        StartCoroutine(Common.delayCoroutine(shakeTimer,()=> { InitShakeCam(); }
        ));

    }

    private void InitShakeCam(NoiseSettings noise=null)
    {
        for (int i = 0; i < 3; i++)
        {
            Debug.Log(noise);
            freeLookCamera.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_NoiseProfile = noise;
        }
    }

    

}
