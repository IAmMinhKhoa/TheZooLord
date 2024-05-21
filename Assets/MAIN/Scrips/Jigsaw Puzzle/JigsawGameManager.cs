using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] SpriteRenderer completeImage;
    [SerializeField] SpriteRenderer hintImage;

    [SerializeField] GameObject parentPieces;
    [SerializeField] List<GameObject> listPuzzlePieces;

    public int levelActive;

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
    }
    // Start is called before the first frame update
    void Start()
    {
        GetPieceFromParent();
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
        isComplete = true;
        winVFX.Play();
        SFXSource.PlayOneShot(clapWin);
    }

    public void BackMenu()
    {
        isComplete = false;
        winVFX.Clear();
        SFXSource.Stop();
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

    void GetPieceFromParent()
    {
        listPuzzlePieces = new List<GameObject>();

        for (int i = 0; i < parentPieces.transform.childCount; i++)
        {
            GameObject child = parentPieces.transform.GetChild(i).gameObject;
            GameObject piece = child.transform.Find("Piece").gameObject;
            listPuzzlePieces.Add(piece);
        }
    }

    public void SetPuzzlePhoto(Image photo, int levelNumber)
    {
        levelActive = levelNumber;
        isComplete = false;
        for (int i = 0; i < listPuzzlePieces.Count; i++)
        {
            listPuzzlePieces[i].GetComponent<SpriteRenderer>().sprite = photo.sprite;
            completeImage.sprite = photo.sprite;
            hintImage.sprite = photo.sprite;
        }
        parentPieces.transform.parent.gameObject.SetActive(true);
    }          

    public void UnlockNewLevel()
    {
        int levelNumber = levelActive;
        if ((levelNumber) >= PlayerPrefs.GetInt("JigsawReachedIndex"))
        {
            PlayerPrefs.SetInt("JigsawReachedIndex", levelNumber + 1);
            PlayerPrefs.SetInt("UnlockedJigsawLevel", PlayerPrefs.GetInt("UnlockedJigsawLevel", 1) + 1);
            PlayerPrefs.Save();

        }
    }

    public bool CheckWinComplete()
    {
        if (!SFXSource.isPlaying)
        {
            return true;
        }
        return false;
    }
}
