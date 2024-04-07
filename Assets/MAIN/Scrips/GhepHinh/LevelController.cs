using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; private set; }
    [SerializeField] Canvas levelMenuCanvas;
    [SerializeField] GameObject[] levelArray;

    public int levelActive;

    [SerializeField] GameObject nextButton;

    [SerializeField] ParticleSystem winVFX;

    bool isCompleteLevel;

    AnimalManager animalManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        GetLevelFromParent();
    }

    private void Update()
    {
        isCompleteLevel = PuzzleManager.instance.isCompleteLevel;
        if(levelActive >= levelArray.Length)
        {
            isCompleteLevel = false;
        }
        SetActiveObject(isCompleteLevel, nextButton);
        Debug.Log(levelActive);
    }

    void GetLevelFromParent()
    {
        levelArray = transform.GetComponentsInChildren<Transform>(true)
                .Where(obj => obj.CompareTag("Level"))
                .Select(obj => obj.gameObject)
                .ToArray();
    }

    public void OnBackLevelMenu()
    {
        //winVFX.Stop(); 
        winVFX.Clear();
        PuzzleManager.instance.isCompleteLevel = false;
        levelArray[levelActive-1].SetActive(false);
        levelMenuCanvas.gameObject.SetActive(true);
    }

    public void OnNextLevel()
    {
        winVFX.Clear();
        PuzzleManager.instance.isCompleteLevel = false;
        levelArray[levelActive-1].SetActive(false);
        levelArray[levelActive].SetActive(true);
        levelActive++;
    }

    void SetActiveObject(bool active, GameObject gameObject)
    {
        if (active)
        {
            gameObject.SetActive(active);
        }
        else
        {
            gameObject.SetActive(active);
        }
    }
}
