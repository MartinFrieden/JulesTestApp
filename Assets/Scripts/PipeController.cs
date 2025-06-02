using UnityEngine;

public class PipeController : MonoBehaviour
{
    public float speed = 5f;
    private float leftEdge;

    void Start()
    {
        // Calculate the left edge of the screen in world coordinates
        // This assumes the camera is at x=0 and orthographic
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f; // -1f for a buffer
    }

    void Update()
    {
        // Move the pipe to the left
        transform.position += Vector3.left * speed * Time.deltaTime;

        // If the pipe is off-screen to the left, destroy it
        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }
}
