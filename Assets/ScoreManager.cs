using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
    {
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    private int currentScore = 0;
    private int highScore = 0;

    private void Start()
        {
        // Load the high score from PlayerPrefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore.ToString();
        }

    private void OnCollisionEnter(Collision collision)
        {
        // Check if the player collides with a point giver
        if (collision.gameObject.CompareTag("PointGiver") && gameObject.CompareTag("Player"))
            {
            // Increase score and update the score text
            currentScore++;
            scoreText.text = "Score: " + currentScore.ToString();

            // Update high score if needed
            if (currentScore > highScore)
                {
                highScore = currentScore;
                highScoreText.text = "High Score: " + highScore.ToString();
                // Save the new high score
                PlayerPrefs.SetInt("HighScore", highScore);
                }

            // Destroy the point giver object
            Destroy(collision.gameObject);
            }
        }
    }
