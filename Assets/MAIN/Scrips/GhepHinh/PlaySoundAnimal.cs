using DG.Tweening;
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

    private Vector3 _originalScale;
    private Vector3 _scaleTo;
    public float scaleSpeed;

    private bool canClick;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        handAnimator = handObject.GetComponent<Animator>();
    }
    private void Start()
    {
        _originalScale = transform.localScale;
        _scaleTo = _originalScale * 1.2f;
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

    public void OnMouseDown()
    {
        if (canClick)
        {
            audioSource.PlayOneShot(soundAnimal);
            DisableClickInteraction();
            transform.DOScale(_scaleTo, scaleSpeed)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    transform.DOScale(_originalScale, scaleSpeed)
                    .OnComplete(() =>
                    {
                        EnableClickInteraction();
                    });
                });

        }


    }

    private System.Collections.IEnumerator PlayDelayedSound(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        audioSource.PlayOneShot(soundName);

        transform.DOScale(_scaleTo, scaleSpeed)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                transform.DOScale(_originalScale, scaleSpeed);

            });

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
