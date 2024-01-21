using UnityEngine;

public class GiftSpawner : MonoBehaviour
    {
    public GameObject giftPrefab; // Assign the gift prefab in the inspector

    public static void SpawnGifts()
        {
        foreach (var spawner in GameObject.FindGameObjectsWithTag("GiftSpawner"))
            {
            var giftSpawner = spawner.GetComponent<GiftSpawner>();
            if (giftSpawner != null)
                {
                giftSpawner.SpawnGift();
                }
            }
        }

    public void SpawnGift()
        {
        if (giftPrefab == null)
            {
            Debug.LogError("Gift prefab is not assigned to the spawner.");
            return;
            }

        Debug.Log($"Spawning gift at {transform.position}");
        Instantiate(giftPrefab, transform.position, Quaternion.identity);
        }
    }
