using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;

    [SerializeField] ParticleSystem winVFX;

    [SerializeField] AudioSource SFXSource;


    [Header("Audio Clip")]
    [SerializeField] AudioClip pickUp;
    [SerializeField] AudioClip dropDownTrue;
    [SerializeField] AudioClip dropDownWrong;
    [SerializeField] AudioClip clapWin;

    [Header("Level Manager")]
    [SerializeField] GameObject level;
    private GameObject[] levelAnimalArray;
    public int levelPress = -1;


    private void Awake()
    {
        instance = this;
        levelAnimalArray = level.GetComponentsInChildren<Transform>(true)
        .Where(obj => obj.CompareTag("Level"))
        .Select(obj => obj.gameObject)
        .ToArray();
    }

    private void Start()
    {
        for (int i = 0; i < levelAnimalArray.Length; i++)
        {
            levelAnimalArray[i].SetActive(false);
        }
    }

    private void Update()
    {
        for (int i = 0; i < levelAnimalArray.Length; i++)
        {
            if(i == levelPress)
            {
                levelAnimalArray[i].SetActive(true);
                break;
            }
        }
    }

    public void PlayPickUp()
    {
        SFXSource.PlayOneShot(pickUp);
    }

    public void PlayDropDownTrue()
    {
        SFXSource.PlayOneShot(dropDownTrue);
    }

    public void PlayDropDownWrong()
    {
        SFXSource.PlayOneShot(dropDownWrong);
    }

    public void PlayClapWin() {
        winVFX.Play();
        SFXSource.PlayOneShot(clapWin);
    }
}
