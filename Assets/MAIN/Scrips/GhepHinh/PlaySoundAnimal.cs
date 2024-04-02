    using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    private bool canClick;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        handAnimator = handObject.GetComponent<Animator>();
    }
    private void Start()
    {

    }

    private void OnEnable()
    {
        DisableClickInteraction();
        StartCoroutine(PlayDelayedSound(5));
    }

    private void Update()
    {
        Debug.Log(handObject.activeSelf);
    }

    public async void OnMouseDown()
    {
        if (canClick)
        {
            audioSource.PlayOneShot(soundAnimal);
            animator.SetTrigger("Complete");
            DisableClickInteraction();
    
            await Task.Delay(2500);

            EnableClickInteraction();

        }


    }

    private System.Collections.IEnumerator PlayDelayedSound(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        audioSource.PlayOneShot(soundName);
        animator.SetTrigger("Complete");

        yield return new WaitForSeconds(1.5f);

        EnableClickInteraction();
    }

    void EnableClickInteraction()
    {
        handObject.SetActive(true);
        canClick = true;
    }

    void DisableClickInteraction()
    {
        handObject.SetActive(false);
        canClick = false;
    }
}
