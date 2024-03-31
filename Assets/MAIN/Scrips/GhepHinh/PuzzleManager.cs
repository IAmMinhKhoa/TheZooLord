using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        instance = this;
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
