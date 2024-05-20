using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    //References
    [Header("UI references")]
    [SerializeField] TMP_Text coinUIText;
    [SerializeField] GameObject animatedCoinPrefab;
    [SerializeField] Transform target;

    [Space]
    [Header("Available coins : (coins to pool)")]
    [SerializeField] int maxCoins;
    Queue<GameObject> coinsQueue = new Queue<GameObject>();


    [Space]
    [Header("Animation settings")]
    [SerializeField][Range(0.5f, 0.9f)] float minAnimDuration;
    [SerializeField][Range(0.9f, 2f)] float maxAnimDuration;

    [SerializeField] Ease easeType;
    [SerializeField] float spread;
    public RectTransform rectCanvas;
    Vector3 targetPosition;
    Vector2 canvasCenter;

    private int _c = 0;
    
    public int Coins
    {
        get { return _c; }
        set
        {
            _c = value;
            //update UI text whenever "Coins" variable is changed
            coinUIText.text = Coins.ToString();
        }
    }

    void Awake()
    {
        
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }


            targetPosition = target.position;

        //prepare pool
        PrepareCoins();
        canvasCenter = new Vector2(rectCanvas.rect.width / 2f, rectCanvas.rect.height / 2f);
        
    }
    private void Start()
    {
        coinUIText.text = Game_Manager.Instance.DataGame.GetCoint().ToString();
    }
    void PrepareCoins()
    {
        GameObject coin;
        for (int i = 0; i < maxCoins; i++)
        {
           
            coin = Instantiate(animatedCoinPrefab);
            coin.transform.parent = transform;
            coin.SetActive(false);
            coinsQueue.Enqueue(coin);
        }
    }

    void Animate( int amount)
    {
     

        for (int i = 0; i < amount; i++)
        {
            // Check if there are coins in the pool
            if (coinsQueue.Count > 0)
            {
                // Extract a coin from the pool
                GameObject coin = coinsQueue.Dequeue();
                coin.SetActive(true);

                // Set the initial position of the coin to the center of the canvas
                coin.GetComponent<RectTransform>().position = canvasCenter  +new Vector2(Random.Range(-spread, spread), 0f); ;
                
                // Animate the coin to the target position
                float duration = Random.Range(minAnimDuration, maxAnimDuration);
                coin.transform.DOMove(targetPosition, duration)
                    .SetEase(easeType)
                    .OnComplete(() =>
                    {
                        // Executes whenever the coin reaches the target position
                        coin.SetActive(false);
                        coinsQueue.Enqueue(coin);
                        this.gameObject.transform.DOScale(1.3f, 0.1f).OnComplete(ResizeScale) ;
                        Coins++;
                    });
            }
        }
    }
    void ResizeScale()
    {
        this.gameObject.transform.DOScale(1, 0.1f);
    }

    public void AddCoins( int amount)
    {
        Animate( amount);
        Game_Manager.Instance.DataGame.AddCoin(amount);
    }
  
}
