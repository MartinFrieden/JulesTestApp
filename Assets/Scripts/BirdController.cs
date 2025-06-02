using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float jumpForce = 5f;
    // public Rigidbody2D rb; // Now fetched in Start

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // GameManager will handle initial kinematic state
        // Bird is made kinematic by GameManager at start
    }

    // Update is called once per frame
    void Update()
    {
        // GameManager handles start input and game state for jumping
        if (GameManager.Instance != null && GameManager.Instance.CurrentState == GameManager.GameState.Playing)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                Jump();
            }
        }
    }

    void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Ensure your pipe prefabs (or their parts that should cause game over) are tagged "Pipe"
        if (collision.gameObject.CompareTag("Pipe"))
        {
            Debug.Log("Collision with Pipe! Game Over.");
            // Here you would typically notify a GameManager to handle the game over state
            if (GameManager.Instance != null) GameManager.Instance.GameOver();
            // enabled = false; // Disable this script to stop bird control
        }
        // Optional: Check for collision with screen boundaries if they have colliders
        // else if (collision.gameObject.CompareTag("Boundary"))
        // {
        //     Debug.Log("Collision with Boundary! Game Over.");
            // if (GameManager.Instance != null) GameManager.Instance.GameOver();
        // }
    }
}
