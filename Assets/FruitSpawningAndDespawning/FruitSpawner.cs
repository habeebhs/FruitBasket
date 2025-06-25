using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject fruitPrefab;
    public float minInterval = 0.25f;
    public float startInterval = 2.5f;
    public float decayRate = 0.8f;
    public int maxNumOfFruits;

    private int spawnCount;
    private float timer;

    private BoxCollider2D spawnCollider;

    void Start()
    {
        maxNumOfFruits = GameManager.Instance.maxFruitsToSpawn;
        spawnCollider = GetComponent<BoxCollider2D>();
        
        if (!spawnCollider)
        {
            Debug.LogError("Missing BoxCollider2D on FruitSpawner!");
        }
    }

    void Update()
    {
        if (spawnCount >= maxNumOfFruits) return;

        float spawnInterval = Mathf.Max(minInterval, startInterval * Mathf.Pow(decayRate, spawnCount));
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnFruit();
            timer = 0f;
            
            print(spawnCount);
            spawnCount++;

        }
    }

    void SpawnFruit()
    {
        Vector2 spawnPosition = GetRandomPointInBounds();
        Instantiate(fruitPrefab, spawnPosition, Quaternion.identity);
    }

    Vector2 GetRandomPointInBounds()
    {
        Bounds bounds = spawnCollider.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = bounds.max.y;
        return new Vector2(x, y);
    }
}
