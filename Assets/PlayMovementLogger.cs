using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class PlayerMovementLogger : MonoBehaviour
    {
    public string playerTag = "Player"; 
    private GameObject player; 
    public float logInterval = 0.5f; 
    private List<string> logData = new List<string>(); // List to store the log entries.
    private string header = "Time,PosX,PosY,PosZ,Forward,Left,Right,Back,Jump"; // Header for the CSV log file.

    private void Start()
        {
        player = GameObject.FindWithTag(playerTag); // Find the player GameObject using the specified tag.
        if (player == null)
            {
            Debug.LogError("Player GameObject with tag '" + playerTag + "' not found."); // Log error if player not found.
            return;
            }

        logData.Add(header); // Add the CSV header to the log data list.
        InvokeRepeating(nameof(LogData), logInterval, logInterval); // Schedule repeated logging at specified intervals.
        }

    private void LogData()
        {
        if (player != null)
            {
            Vector3 position = player.transform.position; // Get player's current position.
            // Create a log entry with time, position, key presses, and point awarded status.
            string logEntry = $"{Time.time:F2}, {position.x:F2}, {position.y:F2}, {position.z:F2}, " +
                              $"{GetKeyPress(KeyCode.W)}, " +
                              $"{GetKeyPress(KeyCode.A)}, " +
                              $"{GetKeyPress(KeyCode.D)}, " +
                              $"{GetKeyPress(KeyCode.S)}, " +
                              $"{GetKeyPress(KeyCode.Space)}, ";
            logData.Add(logEntry); // Add the log entry to the list.
            }
        }

    private string GetKeyPress(KeyCode key)
        {
        return Input.GetKey(key) ? key.ToString() : ""; // Check if the specified key is pressed and return its string representation.
        }

    private void OnDestroy()
        {
        SaveLogData(); 
        }

    private void SaveLogData()
        {
        string filePath = Path.Combine(Application.persistentDataPath, "PlayerMovementLog.csv"); 
        File.WriteAllLines(filePath, logData); 
        Debug.Log("Log saved to: " + filePath); 
        }
    }
