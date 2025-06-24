using UnityEngine;

public class Fruit : MonoBehaviour
{
    public Sprite[] fruitSprites;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = fruitSprites[Random.Range(0, fruitSprites.Length)];
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
