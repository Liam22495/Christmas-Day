using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class PlayerMovementLogger : MonoBehaviour
    {
    public string playerTag = "Player"; // Tag of the player GameObject
    private GameObject player;
    private bool pointAwarded = false;
    public float logInterval = 0.5f; // Time in seconds between logs
    private List<string> logData = new List<string>();
    private string header = "Time,PosX,PosY,PosZ,Forward,Left,Right,Back,Jump,PointAwarded";

    private void Start()
        {
        player = GameObject.FindWithTag(playerTag);
        if (player == null)
            {
            Debug.LogError("Player GameObject with tag '" + playerTag + "' not found.");
            return;
            }

        logData.Add(header); // Add headers to the log
        InvokeRepeating(nameof(LogData), logInterval, logInterval);
        }

    private void LogData()
        {
        if (player != null)
            {
            Vector3 position = player.transform.position;
            string logEntry = $"{Time.time:F2}, {position.x:F2}, {position.y:F2}, {position.z:F2}, " +
                              $"{GetKeyPress(KeyCode.W)}, " +
                              $"{GetKeyPress(KeyCode.A)}, " +
                              $"{GetKeyPress(KeyCode.D)}, " +
                              $"{GetKeyPress(KeyCode.S)}, " +
                              $"{GetKeyPress(KeyCode.Space)}, " +
                              $"{(pointAwarded ? 1 : 0)}";
            logData.Add(logEntry);

            // Reset pointAwarded for the next interval
            pointAwarded = false;
            }
        }

    private string GetKeyPress(KeyCode key)
        {
        return Input.GetKey(key) ? key.ToString() : "";
        }

    private void OnTriggerEnter(Collider other)
        {
        if (other.CompareTag("PointGiver"))
            {
            pointAwarded = true;
            }
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
