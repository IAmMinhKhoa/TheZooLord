using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class ConfigCage : MonoBehaviour
{
    

    public SOAnimal SoAnimal;
    public CinemachineFreeLook cameraCage;
    public List<ConfigAnimal> objAnimals;
    public Transform view_Environment;
    public Transform view_Foods;
    public Transform view_Characteristic;

    public AudioSource audioClipCage;

    public FoodStorage foodStorage;
    private int _currentTargetIndex = 0;
    #region PLAY SOUND

    #endregion
  

    #region SWITCH CAMRERA TO POINT MINI ENVIRONMENT
    [ProButton]
    public void SwitchToViewEnvironment()
    {
        SetTartgetCam(view_Environment);
    }
    [ProButton]
    public void SwitchToViewFoods()
    {
        SetTartgetCam(view_Foods);
    }
    [ProButton]
    public void SwitchToViewCharacteristic()
    {
        SetTartgetCam(view_Characteristic);
    }
    #endregion

    #region Switch Camera to Animals
    public void OpenViewCage()
    {
        cameraCage.gameObject.SetActive(true);
        SwitchToNextTarget();
    }
    public void CloseViewCage()
    {
        cameraCage.gameObject.SetActive(false);
    }
    public void SwitchToNextTarget()
    {
        _currentTargetIndex++;
        if (_currentTargetIndex >= objAnimals.Count)
        {
            _currentTargetIndex = 0;
        }

        SetCameraTarget();
    }
    
    public void SwitchToPreviousTarget()
    {
        _currentTargetIndex--;
        if (_currentTargetIndex < 0)
        {
            _currentTargetIndex = objAnimals.Count - 1;
        }

        SetCameraTarget();
    }

    private void SetCameraTarget()
    {
        if (_currentTargetIndex >= 0 && _currentTargetIndex < objAnimals.Count)
        {
            Transform targetTransform = objAnimals[_currentTargetIndex].transform;
            SetTartgetCam(targetTransform);
        }
    }
    #endregion

    public void SetTartgetCam(Transform target)
    {
        cameraCage.gameObject.SetActive(true);
        cameraCage.Follow = target;
        cameraCage.LookAt = target;
    }
}
