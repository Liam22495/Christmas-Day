using UnityEngine;

public class Rotator : MonoBehaviour
    {
    public float rotationSpeed = 100f; // Speed of rotation

    void Update()
        {
        // Rotate objects tagged with "RotateY" around the Y-axis
        RotateObjects("RotatingY", Vector3.up);

        // Rotate objects tagged with "RotateX" around the X-axis
        RotateObjects("RotatingX", Vector3.right);
        }

    void RotateObjects(string tag, Vector3 axis)
        {
        GameObject[] rotatingObjects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in rotatingObjects)
            {
            // Rotate each object around the specified axis at the specified speed
            obj.transform.Rotate(axis, rotationSpeed * Time.deltaTime);
            }
        }

    public void AdjustSpeed(float newSpeed)
        {
        rotationSpeed = newSpeed;
        }
    }
