using UnityEngine;
using UnityEngine.InputSystem;

public class BasketController : MonoBehaviour
{
    public float speed = 5f;
    public float boundary = 8f;
    public int fruitCount = 0;
    public AudioSource catchSound;

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
        controls.Disable();
    }

    private void Start()
    {
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
            GameManager.Instance.score = fruitCount;
            Destroy(other.gameObject);
            Debug.Log("Fruit collected! Total: " + fruitCount);
            catchSound.pitch = Random.Range(0.8f ,1.2f);
            catchSound.Play();
        }
    }
}
