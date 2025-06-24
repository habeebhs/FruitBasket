using UnityEngine;

public class FruitDespawner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(other.gameObject);
    }

}
