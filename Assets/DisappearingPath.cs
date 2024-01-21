using System.Collections;
using UnityEngine;

public class DisappearingPath : MonoBehaviour
    {
    public string disappearingPathTag = "DisappearingPath"; // Tag for disappearing blocks
    public float delayBeforeInactive = 2.0f; // Time to wait before the block becomes inactive
    public float inactiveTime = 2.0f; // Time for which the block remains inactive
    public float reactivationTime = 2.0f; // Time over which the block reactivates

    private void OnTriggerEnter(Collider other)
        {
        // Check if the object that triggered the event has the disappearingPathTag
        if (other.CompareTag(disappearingPathTag))
            {
            StartCoroutine(DeactivateAndReactivate(other.gameObject));
            }
        }

    private IEnumerator DeactivateAndReactivate(GameObject block)
        {
        // Wait for the specified delay time before deactivating the block
        yield return new WaitForSeconds(delayBeforeInactive);

        // Get the renderer and collider components
        var renderer = block.GetComponent<Renderer>();
        var collider = block.GetComponent<Collider>();

        // Deactivate the block's renderer and collider
        if (renderer != null)
            {
            renderer.enabled = false;
            }
        if (collider != null)
            {
            collider.enabled = false;
            }

        // Wait for the specified inactive time
        yield return new WaitForSeconds(inactiveTime);

        // Reactivate the block's renderer and collider
        if (renderer != null)
            {
            renderer.enabled = true;
            }
        if (collider != null)
            {
            collider.enabled = true;
            }

        // Additional effects can be added here
        yield return new WaitForSeconds(reactivationTime);
        }
    }
