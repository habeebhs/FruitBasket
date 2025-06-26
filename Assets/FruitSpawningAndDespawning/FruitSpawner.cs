using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FruitSpawner : MonoBehaviour
{
    public GameObject fruitPrefab;
    public GameObject bombPrefab;
    public float minInterval = 0.25f;
    public float startInterval = 2.5f;
    public float decayRate = 0.8f;
    public int maxNumOfFruits;

    private int spawnCount;
    private float timer;
    private bool gameOverStarted = false;

    private BoxCollider2D spawnCollider;

    void Start()
    {
        maxNumOfFruits = GameManager.Instance.maxFruitsToSpawn;
        spawnCollider = GetComponent<BoxCollider2D>();

        if (!spawnCollider)
        {
            Debug.LogError("Missing BoxCollider2D on FruitSpawner!");
            GameManager.Instance.fruitSpawner = this.gameObject;
        }
    }

    void Update()
    {
        if (spawnCount >= maxNumOfFruits)
        {
            if (!gameOverStarted)
            {
                gameOverStarted = true;
                StartCoroutine(OpenGameOverScreen());
            }
            return; // Stop spawning once max is reached
        }

        float spawnInterval = Mathf.Max(minInterval, startInterval * Mathf.Pow(decayRate, spawnCount));
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnFruit();
            TryToSpawnBomb();
            timer = 0f;

            Debug.Log(spawnCount);
            spawnCount++;
        }
    }

    void SpawnFruit()
    {
        Vector2 spawnPosition = GetRandomPointInBounds();
        Instantiate(fruitPrefab, spawnPosition, Quaternion.identity);
    }

    void TryToSpawnBomb()
    {
        int randomNum = Random.Range(0, 20);
        if (randomNum == 3)
        {
            Vector2 spawnPosition = GetRandomPointInBounds();
            Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
        }
    }

    Vector2 GetRandomPointInBounds()
    {
        Bounds bounds = spawnCollider.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = bounds.max.y;
        return new Vector2(x, y);
    }

    IEnumerator OpenGameOverScreen()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("GameOver_Scene");
    }
}
