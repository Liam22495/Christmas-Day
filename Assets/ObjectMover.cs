using UnityEngine;
using System.Collections;

public class ObjectMover : MonoBehaviour
    {
    public float startZ = 494f; // Starting Z position
    public float endZ = 513f; // Ending Z position
    public float speed = 1f; // Speed of the movement

    private void Start()
        {
        StartCoroutine(MoveObject());
        }

    private IEnumerator MoveObject()
        {
        while (true) // Repeat forever
            {
            // Move from startZ to endZ
            yield return StartCoroutine(MoveToPosition(endZ));

            // Move from endZ back to startZ
            yield return StartCoroutine(MoveToPosition(startZ));
            }
        }

    private IEnumerator MoveToPosition(float targetZ)
        {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(transform.position.x, transform.position.y, targetZ);

        float journeyLength = Vector3.Distance(startPosition, endPosition);
        float journey = 0f;

        while (journey < journeyLength)
            {
            journey += speed * Time.deltaTime;
            float fraction = journey / journeyLength;
            transform.position = Vector3.Lerp(startPosition, endPosition, fraction);
            yield return null;
            }
        }
    }
