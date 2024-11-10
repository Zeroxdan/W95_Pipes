// 09.11.2024 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using System;
using UnityEditor;
using UnityEngine;

public class ConstrainObject : MonoBehaviour
{
    public BoxCollider boundingBox; // Reference to the bounding box collider

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the bounding box
        if (collision.collider == boundingBox)
        {
            // Determine the direction of the collision
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 objectPosition = transform.position;

            // Simple logic to push the object back within bounds when it hits the bounding box
            if (objectPosition.x < boundingBox.bounds.min.x || objectPosition.x > boundingBox.bounds.max.x)
            {
                transform.position = new Vector3(Mathf.Clamp(objectPosition.x, boundingBox.bounds.min.x, boundingBox.bounds.max.x), objectPosition.y, objectPosition.z);
            }

            if (objectPosition.y < boundingBox.bounds.min.y || objectPosition.y > boundingBox.bounds.max.y)
            {
                transform.position = new Vector3(objectPosition.x, Mathf.Clamp(objectPosition.y, boundingBox.bounds.min.y, boundingBox.bounds.max.y), objectPosition.z);
            }

            if (objectPosition.z < boundingBox.bounds.min.z || objectPosition.z > boundingBox.bounds.max.z)
            {
                transform.position = new Vector3(objectPosition.x, objectPosition.y, Mathf.Clamp(objectPosition.z, boundingBox.bounds.min.z, boundingBox.bounds.max.z));
            }
        }
    }
}
