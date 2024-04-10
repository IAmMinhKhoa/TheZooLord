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

    [HideInInspector] public Transform view_Environment;
    [HideInInspector] public Transform view_Foods;
    [HideInInspector] public Transform view_Characteristic;
    [HideInInspector] public Transform view_Conservation;

    public AudioSource audioClipCage;

    public FoodStorage foodStorage;
    private int _currentTargetIndex = 0;

    [SerializeField] private SpriteRenderer iconMarkMap;
    private void Start()
    {
        iconMarkMap.sprite = SoAnimal.icon;
        //Set target Friend Animal
        objAnimals[0].GetComponent<AnimalController>().otherAnimal = objAnimals[1].transform;
        objAnimals[1].GetComponent<AnimalController>().otherAnimal = objAnimals[0].transform;
    }
    #region PLAY SOUND
    public void PlaySoundType(SoundTypeInCage typeSound)
    {
        switch (typeSound)
        {
            case SoundTypeInCage.Chirp:
                setSoundToAudio(SoAnimal.voiceChirp);
                break;
            case SoundTypeInCage.Environment:
                setSoundToAudio(SoAnimal.dataEnvironment.voice);
                break;
            case SoundTypeInCage.Food:
                //FOODS SOUND
                break;
            case SoundTypeInCage.Characteristic:
                setSoundToAudio(SoAnimal.dataCharacteristic.voice);
                break;
            case SoundTypeInCage.Conservationlevel:
                setSoundToAudio(SoAnimal.dataConservationlevel.voice);
                break;
            default:
                break;
        }
    }
    public void setSoundToAudio(AudioClip src)
    {
        audioClipCage.clip = src;
        audioClipCage.Play();
    }
    #endregion
    public List<SOFood> GetSOfoods()
    {
        return SoAnimal.dataFoods.SoFoods;
    }
    public GameObject InstancePrefab(GameObject prefab, Transform transform)
    {
        return Instantiate(prefab, transform);
    }
    public void ResetChildObjectView()
    {
        DelAllChildObject(view_Foods);
        DelAllChildObject(view_Conservation);
        DelAllChildObject(view_Characteristic);
        DelAllChildObject(view_Environment);
    }
    private void DelAllChildObject(Transform parent)
    {
        int childCount = parent.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            GameObject childObject = parent.GetChild(i).gameObject;
            Destroy(childObject);
        }
    }
    public void SetTargetMiniEnvironment(Transform target_1, Transform target_2, Transform target_3, Transform target_4)
    {
        view_Environment = target_1;
        view_Foods = target_2;
        view_Characteristic = target_3;
        view_Conservation = target_4;

    }
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
    
    public void SwitchToViewConservation()
    {
        SetTartgetCam(view_Conservation);
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
