    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundAnimal : MonoBehaviour
{
    [Header("Sound Animal")]
    [SerializeField] AudioClip soundAnimal;
    [SerializeField] AudioClip soundName;
    [SerializeField] AudioSource audioSource;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();       
    }
    private void Start()
    {
       
        StartCoroutine(PlayDelayedSound(2));
    }
    public void OnMouseDown()
    {
        audioSource.PlayOneShot(soundAnimal);
    }

    private System.Collections.IEnumerator PlayDelayedSound(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        audioSource.PlayOneShot(soundName);
        animator.SetTrigger("Complete");
    }
}
