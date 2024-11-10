// 09.11.2024 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

//using System;
using UnityEditor;
using UnityEngine;

public class PipeGeneratorAI : MonoBehaviour
{
    public GameObject pipePrefab; // Assign your pipe prefab in the Inspector
    public BoxCollider boundingBox; // Assign the bounding box collider in the Inspector
    public int numberOfPipes = 10; // Number of pipe segments to generate
    public float pipeLength = 1.0f; // Length of each pipe segment

    void Start()
    {
        GeneratePipes();
    }

    void GeneratePipes()
    {
        for (int i = 0; i < numberOfPipes; i++)
        {
            Vector3 randomPosition = GetRandomPositionWithinBounds();
            Instantiate(pipePrefab, randomPosition, Quaternion.identity);
        }
    }

    Vector3 GetRandomPositionWithinBounds()
    {
        Bounds bounds = boundingBox.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        float z = Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3(x, y, z);
    }
}
