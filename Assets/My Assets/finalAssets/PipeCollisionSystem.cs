using UnityEngine;

public class PipeCollisionSystem : MonoBehaviour
{

    public BoxCollider boundingBox;
    public Bounds BoxBounds; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BoxBounds = boundingBox.bounds;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
