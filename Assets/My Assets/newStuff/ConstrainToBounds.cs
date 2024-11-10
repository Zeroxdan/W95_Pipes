// 09.11.2024 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using System;
using UnityEditor;
using UnityEngine;

public class ConstrainToBounds : MonoBehaviour
{
    public BoxCollider boundingBox; // Assign the bounding box collider in the Inspector

    void Update()
    {
        Vector3 clampedPosition = transform.position;

        // Calculate the min and max bounds
        Vector3 minBounds = boundingBox.bounds.min;
        Vector3 maxBounds = boundingBox.bounds.max;

        // Clamp the object's position to stay within the bounds
        clampedPosition.x = Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x);
        clampedPosition.y = Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y);
        clampedPosition.z = Mathf.Clamp(transform.position.z, minBounds.z, maxBounds.z);

        // Apply the clamped position
        transform.position = clampedPosition;
    }
}
