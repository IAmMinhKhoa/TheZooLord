    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundAnimal : MonoBehaviour
{
    [Header("Sound Animal")]
    [SerializeField] AudioClip soundAnimal;
    [SerializeField] AudioClip soundName;
    [SerializeField] AudioSource audioSource;

    [SerializeField] GameObject handObject;

    Animator animator;
    Animator handAnimator;

    private bool canClick = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        handAnimator = handObject.GetComponent<Animator>();
    }
    private void Start()
    {
        handObject.SetActive(false);
        StartCoroutine(PlayDelayedSound(3));
    }

    private void Update()
    {
        if (!canClick && !audioSource.isPlaying && !animator.GetCurrentAnimatorStateInfo(0).IsName("YourAnimationName"))
        {
            canClick = true;
            handObject.SetActive(true);
        }
    }

    public void OnMouseDown()
    {
        if (canClick)
        {
            handObject.SetActive(false);
            audioSource.PlayOneShot(soundAnimal);
            animator.SetTrigger("Complete");

            canClick = false;
        }
    }

    private System.Collections.IEnumerator PlayDelayedSound(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        audioSource.PlayOneShot(soundName);
        animator.SetTrigger("Complete");
        handObject.SetActive(true);
    }
}
