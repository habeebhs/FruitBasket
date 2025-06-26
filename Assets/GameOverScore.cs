using TMPro;
using UnityEngine;

public class GameOverScore : MonoBehaviour
{
    public TextMeshProUGUI gameOverScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (gameOverScore)
        { 
            gameOverScore.text = GameManager.Instance.score.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
