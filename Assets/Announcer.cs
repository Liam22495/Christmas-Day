using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Announcer : MonoBehaviour
    {
    public Transform respawnPoint;
    public GameObject gameManager; // Assign this in the Unity Inspector

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText; // Assuming you have a TextMeshPro element for the high score
    public GameObject canvas; // This is your canvas GameObject
    public float typingSpeed = 0.05f;
    private int totalGifts;

    private void Start()
        {
        totalGifts = GameObject.FindGameObjectsWithTag("PointGiver").Length;
        canvas.SetActive(false); // Disable the canvas at the start
        }

    private void OnCollisionEnter(Collision collision)
        {
        if (collision.gameObject.CompareTag("Ground"))
            {
            RespawnPlayer();
            ResetScore();
            StartCoroutine(DisplayChatBoxWithFailureMessage());
            }
        else if (collision.gameObject.CompareTag("Tree"))
            {
            StartCoroutine(DisplayChatBoxWithTreeCollisionMessage());
            StartCoroutine(RestartGameAfterDelay(15f)); // Restart game after 15 seconds
            }
        }

    void RespawnPlayer()
        {
        transform.position = respawnPoint.position;
        }

    void ResetScore()
        {
        scoreText.text = "0";
        }

    IEnumerator RestartGameAfterDelay(float delay)
        {
        yield return new WaitForSeconds(delay);
        DestroyRemainingGifts();
        yield return StartCoroutine(RespawnGifts()); // Wait for respawn after destruction
        }


    void DestroyRemainingGifts()
        {
        GameObject[] gifts = GameObject.FindGameObjectsWithTag("PointGiver");
        foreach (GameObject gift in gifts)
            {
            Destroy(gift);
            }
        }

    IEnumerator RespawnGifts()
        {
        yield return new WaitForSeconds(1f); // 1-second delay before respawning

        GiftSpawner.SpawnGifts(); // Call static method to spawn gifts from all spawners
        }


    IEnumerator DisplayChatBoxWithFailureMessage()
        {
        yield return StartCoroutine(DisplayMessage($"Oh no, you have failed the obby section. You still have {totalGifts} gifts to collect. Don't worry this is only a learning based game to enhance skills and discover new techniques."));
        canvas.SetActive(false); // Disable the canvas after 15 seconds
        }

    IEnumerator DisplayChatBoxWithTreeCollisionMessage()
        {
        string message;
        if (totalGifts > 0)
            {
            message = $"Good effort! Your total score is {scoreText.text} while holding a high score of {highScoreText.text}, sadly you have missed {totalGifts}, the game will restart shortly and you can try again!";
            }
        else
            {
            message = $"Congratulations! Your total score is {scoreText.text} while holding a high score of {highScoreText.text}, and you have missed {totalGifts}. Good job! The game will restart shortly and you can improve your skills.";
            }

        yield return StartCoroutine(DisplayMessage(message));
        canvas.SetActive(false); // Disable the canvas after 15 seconds
        }

    IEnumerator DisplayMessage(string message)
        {
        canvas.SetActive(true); // Enable the canvas
        TextMeshProUGUI chatText = canvas.GetComponentInChildren<TextMeshProUGUI>(true);
        chatText.text = "";

        foreach (char letter in message.ToCharArray())
            {
            chatText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
            }

        yield return new WaitForSeconds(15);
        }
    }
