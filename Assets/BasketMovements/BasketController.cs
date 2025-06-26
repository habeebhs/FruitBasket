using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BasketController : MonoBehaviour
{
    public float speed = 5f;
    public float boundary = 8f;
    public static int fruitCount = 0;
    public GameObject[] fruitVisualPrefabs;
    public Transform pileAnchor;

    private Rigidbody2D rb;
    private float horizontalInput = 0f;

    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx =>
        {
            horizontalInput = ctx.ReadValue<Vector2>().x;
        };

        controls.Player.Move.canceled += ctx =>
        {
            horizontalInput = 0f;
        };
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        if (controls != null)
        {
            controls.Disable();
        }
        
    }

    private void Start()
    {
        fruitCount = 0;
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody2D found on Basket GameObject!");
            enabled = false;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);

        float clampedX = Mathf.Clamp(transform.position.x, -boundary, boundary);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        Debug.Log("Input: " + horizontalInput);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fruit"))
        {
            fruitCount++;
            Destroy(other.gameObject);
            Debug.Log("Fruit collected! Total: " + fruitCount);

            //Stacking the basket

            if (fruitVisualPrefabs.Length > 0 && pileAnchor != null)
            {
                int index = Random.Range(0, fruitVisualPrefabs.Length);
                GameObject prefabToUse = fruitVisualPrefabs[index];

                // Layer-based positioning (keep your working code!)
                int fruitsPerLayer = 15;
                int layer = (fruitCount - 1) / fruitsPerLayer;
                float radius = 0.8f + 0.03f * layer;
                float angle = Random.Range(0f, 2f * Mathf.PI);
                float xOffset = (radius + Random.Range(-0.05f, 0.05f)) * Mathf.Cos(angle);
                float zOffset = (radius + Random.Range(-0.05f, 0.05f)) * Mathf.Sin(angle);
                float yOffset = 0.12f * layer + Random.Range(-0.01f, 0.01f);
                Vector3 pilePos = pileAnchor.position + new Vector3(xOffset, yOffset, zOffset);

                Quaternion randomRot = Quaternion.Euler(0, 0, Random.Range(-20f, 20f));

                GameObject fruit = Instantiate(prefabToUse, pilePos, randomRot, pileAnchor);
                //fruit.transform.localScale = Vector3.one * 0.9f; // Optional variety
            }

        }

        else if (other.CompareTag("Bomb"))
        {
            print("GameOver");
            SceneManager.LoadScene("GameOver_Scene");
        }
    }

}
