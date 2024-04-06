using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawGameManager : MonoBehaviour
{
    public static JigsawGameManager instance;
    [SerializeField] List<GameObject> listJigsawPieces;

    [SerializeField] ParticleSystem winVFX;

    [SerializeField] AudioSource SFXSource;


    [Header("Audio Clip")]
    [SerializeField] AudioClip pickUp;
    [SerializeField] AudioClip dropDownTrue;
    [SerializeField] AudioClip dropDownWrong;
    [SerializeField] AudioClip clapWin;

    JigsawPieces jigsawPieces;

    public bool isComplete = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
        jigsawPieces = FindObjectOfType<JigsawPieces>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

    public void PlayClapWin()
    {
        winVFX.Play();
        SFXSource.PlayOneShot(clapWin);
    }

    //public void SetActiveLevel(int levelPress)
    //{
    //    for (int i = 0; i < levelAnimalArray.Length; i++)
    //    {
    //        if (i == levelPress - 1)
    //        {
    //            levelAnimalArray[i].SetActive(true);
    //            break;
    //        }
    //    }
    //}

    public bool CheckWinComplete()
    {
        if (!SFXSource.isPlaying)
        {
            return true;
        }
        return false;
    }
}
