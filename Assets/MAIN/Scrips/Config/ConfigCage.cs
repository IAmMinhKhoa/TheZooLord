using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class ConfigCage : MonoBehaviour
{
    public CinemachineFreeLook cameraCage;
    public List<ConfigAnimal> objAnimals;
    public FoodStorage foodStorage;
    private int currentTargetIndex = 0;

    public void OpenViewCage()
    {
        cameraCage.gameObject.SetActive(true);
        SwitchToNextTarget();
    }
    public void CloseViewCage()
    {
        cameraCage.gameObject.SetActive(false);
    }

    #region Switch Camera to Animals
    [ProButton]
    public void SwitchToNextTarget()
    {

        currentTargetIndex++;
        if (currentTargetIndex >= objAnimals.Count)
        {
            currentTargetIndex = 0;
        }

        SetCameraTarget();
    }
    [ProButton]
    public void SwitchToPreviousTarget()
    {
        currentTargetIndex--;
        if (currentTargetIndex < 0)
        {
            currentTargetIndex = objAnimals.Count - 1;
        }

        SetCameraTarget();
    }

    private void SetCameraTarget()
    {
        if (currentTargetIndex >= 0 && currentTargetIndex < objAnimals.Count)
        {
            Transform targetTransform = objAnimals[currentTargetIndex].transform;
            cameraCage.Follow = targetTransform;
            cameraCage.LookAt = targetTransform;
        }
    }
    #endregion

}
