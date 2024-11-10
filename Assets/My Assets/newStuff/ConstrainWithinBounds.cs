// 09.11.2024 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using System;
using UnityEditor;
using UnityEngine;

public class ConstrainWithinBounds : MonoBehaviour
{
    public BoxCollider boundingBox; // Reference to the bounding box collider

    private void Update()
    {
        Vector3 position = transform.position;

        // Get the bounds of the collider
        Bounds bounds = boundingBox.bounds;

        // Clamp the position within the bounding box bounds
        position.x = Mathf.Clamp(position.x, bounds.min.x, bounds.max.x);
        position.y = Mathf.Clamp(position.y, bounds.min.y, bounds.max.y);
        position.z = Mathf.Clamp(position.z, bounds.min.z, bounds.max.z);

        transform.position = position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If you need to handle specific collisions, you can add logic here
        Debug.Log("Collided with: " + collision.collider.name);
    }
}
