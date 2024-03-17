using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewAnimals : MonoBehaviour
{
    public ConfigCage configCage;
    public Button btnOut;
    public Button btnNext;
    public Button btnPrevious;

    private int currentTargetIndex = 0;

    private void Start()
    {
        btnNext.onClick.AddListener(SwitchToNextTarget);
        btnPrevious.onClick.AddListener(SwitchToPreviousTarget);
    }


    public void OpenViewAnimals()
    {
        configCage.cameraCage.gameObject.SetActive(true);
    }
    public void CloseViewAnimals()
    {
        configCage.cameraCage.gameObject.SetActive(false);
    }

    #region Switch Camera to Animals
    [ProButton]
    public void SwitchToNextTarget()
    {

        currentTargetIndex++;
        if (currentTargetIndex >= configCage.objAnimals.Count)
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
            currentTargetIndex = configCage.objAnimals.Count - 1;
        }

        SetCameraTarget();
    }

    private void SetCameraTarget()
    {
        if (currentTargetIndex >= 0 && currentTargetIndex < configCage.objAnimals.Count)
        {
            Transform targetTransform = configCage.objAnimals[currentTargetIndex].transform;
            configCage.cameraCage.Follow = targetTransform;
            configCage.cameraCage.LookAt = targetTransform;
        }
    }
    #endregion
}
