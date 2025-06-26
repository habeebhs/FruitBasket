using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("GameObjects")]
    public GameObject fruitSpawner;

    [Header("Game Settings")]
    public int maxFruitsToSpawn = 100;
    public int score = 0;

    [Header("UI")]
    public TextMeshProUGUI FruitScoreTextUI;


    void Awake()
    {
        // Singleton enforcement
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Optional: persists across scenes
    }

    void Start()
    {
        //fruitSpawner = GameObject.Find("FruitSpawner");
    }

    void Update()
    {
        score = BasketController.fruitCount;
        if (FruitScoreTextUI)
        {
            FruitScoreTextUI.text = BasketController.fruitCount + " / " + maxFruitsToSpawn;
        }

        else
        {
            if (GameObject.Find("FruitScoreText"))
            { 
                FruitScoreTextUI = GameObject.Find("FruitScoreText").GetComponent<TextMeshProUGUI>();
            }
            
        }
    }
}
