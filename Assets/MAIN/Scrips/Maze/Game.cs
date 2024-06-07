using Cinemachine;
using GG.Infrastructure.Utils.Swipe;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Game : MonoBehaviour
{
    public static Game Instance;

    [SerializeField] private SwipeListener swipeListener;

    [Header("Gameobject")]
    public CinemachineVirtualCamera vcam;
    public Transform Player;
    public Transform Goal;
    public Transform Walls;
    public GameObject WallTemplate;
    public GameObject FloorTemplate;
    public float MovementSmoothing;

    [Header("List Animal SO")]
    [SerializeField] List<SOAnimal> listSOAnimal;
    int animalSOIndex;

    [Header("Maze Size")]
    public int Width = 3;
    public int Height = 3;
    public bool[,] HWalls, VWalls;
    public float HoleProbability;
    public int GoalX, GoalY;

    private int PlayerX, PlayerY;

    [SerializeField] private int baseSize = 3; // Kích thước mê cung cơ bản
    [SerializeField] private int sizeIncrement = 1; // Số lượng ô tăng thêm cho mỗi cấp độ
    public int levelCurrent;

    [SerializeField] TextMeshProUGUI levelText;

    public bool isBack = false;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else 
            Destroy(Instance);
    }

    void Start()
    {
        StartNext(0);
        swipeListener.OnSwipe.AddListener(OnSwipe);
        animalSOIndex = 0;
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A) && !HWalls[PlayerX, PlayerY])
        //    PlayerX--;
        //if (Input.GetKeyDown(KeyCode.D) && !HWalls[PlayerX + 1, PlayerY])
        //    PlayerX++;
        //if (Input.GetKeyDown(KeyCode.W) && !VWalls[PlayerX, PlayerY + 1])
        //    PlayerY++;
        //if (Input.GetKeyDown(KeyCode.S) && !VWalls[PlayerX, PlayerY])
        //    PlayerY--;

        //Vector3 target = new Vector3(PlayerX + 0.5f, PlayerY + 0.5f);

        //Player.transform.position = Vector3.Lerp(Player.transform.position, target, Time.deltaTime * MovementSmoothing);

        //if (Vector3.Distance(Player.transform.position, new Vector3(GoalX + 0.5f, GoalY + 0.5f)) < 0.12f)
        //{
        //    NextLevel();
        //}
        //if (Input.GetKeyDown(KeyCode.G))
        //    StartNext();
    }

    private void OnSwipe(string swipe)
    {
        Debug.Log(swipe);
        switch (swipe)
        {
            case "Left":
                if (!HWalls[PlayerX, PlayerY])
                    PlayerX--;
                break;
            case "Right":
                if (!HWalls[PlayerX + 1, PlayerY])
                    PlayerX++;
                break;
            case "Up":
                if (!VWalls[PlayerX, PlayerY + 1])
                    PlayerY++;
                break;
            case "Down":
                if (!VWalls[PlayerX, PlayerY])
                    PlayerY--;
                break;
        }

        Vector3 target = new Vector3(PlayerX + 0.5f, PlayerY + 0.5f);

        //Player.transform.position = Vector3.Lerp(Player.transform.position, target, Time.deltaTime * MovementSmoothing);
        Player.transform.position = target;
        if (Vector3.Distance(Player.transform.position, new Vector3(GoalX + 0.5f, GoalY + 0.5f)) < 0.12f)
        {
            SoundManager.instance.PlaySound(SoundType.Success);

            NextLevel();


        }
    }

    void SetIcon()
    {
        if(animalSOIndex > listSOAnimal.Count - 1) 
        {
            animalSOIndex = 0;
        }
        for(int i = 0; i < listSOAnimal.Count; i++) 
        {
            
            if (i == animalSOIndex)
            {
                Sprite iconAnimal = listSOAnimal[i].icon;   
                Sprite iconFood = listSOAnimal[i].dataFoods.SoFoods[Random.Range(0, 2)].iconFood;

                Player.gameObject.GetComponent<SpriteRenderer>().sprite = iconAnimal;
                Goal.gameObject.GetComponent<SpriteRenderer>().sprite = iconFood;
                animalSOIndex = i + 1;
                break;
            }
        }    
    }

    public int Rand(int max)
    {
        return UnityEngine.Random.Range(0, max);
    }
    public float frand()
    {
        return UnityEngine.Random.value;
    }

    public void ActiveMaze(int size)
    {
        levelCurrent = size;           
        StartNext(levelCurrent);
    }

    void NextLevel()
    {
        UnlockNewLevel();
        levelCurrent++;
        levelText.text = "Level " + levelCurrent;
        StartNext(levelCurrent);
    }

    public void UnlockNewLevel()
    {
        if ((levelCurrent) >= PlayerPrefs.GetInt("MazeReachedIndex"))
        {
            PlayerPrefs.SetInt("MazeReachedIndex", levelCurrent + 1);
            PlayerPrefs.SetInt("UnlockedMazeLevel", PlayerPrefs.GetInt("UnlockedMazeLevel", 1) + 1);
            PlayerPrefs.Save();

        }
    }

    public void StartNext(int level)
    {
        SetIcon();
        foreach (Transform child in Walls)
            Destroy(child.gameObject);

        int mazeSize = baseSize + (level / 2) * sizeIncrement;
        Width = mazeSize;
        Height = mazeSize;  
        (HWalls, VWalls) = GenerateLevel(Width, Height);
        int cornerIndex = Random.Range(0, 4);
        //PlayerX = Rand(Width);
        //PlayerY = Rand(Height);

        //int minDiff = Mathf.Max(Width, Height) / 2;
        //while (true)
        //{
        //    GoalX = Rand(Width);
        //    GoalY = Rand(Height);
        //    if (Mathf.Abs(GoalX - PlayerX) >= minDiff) break;
        //    if (Mathf.Abs(GoalY - PlayerY) >= minDiff) break;
        //}

        for (int x = 0; x < Width + 1; x++)
            for (int y = 0; y < Height; y++)
                if (HWalls[x, y])
                    Instantiate(WallTemplate, new Vector3(x, y + 0.5f, 0), Quaternion.Euler(0, 0, 90), Walls);
        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height + 1; y++)
                if (VWalls[x, y])
                    Instantiate(WallTemplate, new Vector3(x + 0.5f, y, 0), Quaternion.identity, Walls);
        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
                Instantiate(FloorTemplate, new Vector3(x + 0.5f, y + 0.5f), Quaternion.identity, Walls);

        //Player.transform.position = new Vector3(PlayerX + 0.5f, PlayerY + 0.5f);
        //Goal.transform.position = new Vector3(GoalX + 0.5f, GoalY + 0.5f);
        switch (cornerIndex)
        {
            case 0: // Góc trên bên trái
                Player.transform.position = new Vector3(0.5f, 0.5f);
                PlayerX = 0;
                PlayerY = 0;
                Goal.transform.position = new Vector3(Width - 0.5f, Height - 0.5f);
                GoalX = Width - 1;
                GoalY = Height - 1;
                break;
            case 1: // Góc trên bên phải
                Player.transform.position = new Vector3(Width - 0.5f, 0.5f);
                PlayerX = Width - 1;
                PlayerY = 0;
                Goal.transform.position = new Vector3(0.5f, Height - 0.5f);
                GoalX = 0;
                GoalY = Height - 1;
                break;
            case 2: // Góc dưới bên trái
                Player.transform.position = new Vector3(0.5f, Height - 0.5f);
                PlayerX = 0;
                PlayerY = Height - 1;
                Goal.transform.position = new Vector3(Width - 0.5f, 0.5f);
                GoalX = Width - 1;
                GoalY = 0;
                break;
            case 3: // Góc dưới bên phải
                Player.transform.position = new Vector3(Width - 0.5f, Height - 0.5f);
                PlayerX = Width - 1;
                PlayerY = Height - 1;
                Goal.transform.position = new Vector3(0.5f, 0.5f);
                GoalX = 0;
                GoalY = 0;
                break;
        }

        vcam.m_Lens.OrthographicSize = Mathf.Pow(Mathf.Max(Width / 1.5f, Height), 0.70f) * 1f;
    }

    public (bool[,], bool[,]) GenerateLevel(int w, int h)
    {
        bool[,] hwalls = new bool[w + 1, h];
        bool[,] vwalls = new bool[w, h + 1];

        bool[,] visited = new bool[w, h];
        bool dfs(int x, int y)
        {
            if (visited[x, y])
                return false;
            visited[x, y] = true;

            var dirs = new[]
            {
                (x - 1, y, hwalls, x, y),
                (x + 1, y, hwalls, x + 1, y),
                (x, y - 1, vwalls, x, y),
                (x, y + 1, vwalls, x, y + 1),
            };

            foreach (var (nx, ny, wall, wx, wy) in dirs.OrderBy(t => frand()))
                wall[wx, wy] = !(0 <= nx && nx < w && 0 <= ny && ny < h && (dfs(nx, ny) || frand() < HoleProbability));

            return true;
        }
        dfs(0, 0);

        return (hwalls, vwalls);
    }
}
