using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    private bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bird entered the trigger and it hasn't been triggered yet for this pipe
        if (!triggered && other.gameObject.CompareTag("Player")) // Assuming bird is tagged "Player"
        {
            triggered = true; // Prevent multiple scores from the same pipe trigger
            if (GameManager.Instance != null)
            {
                GameManager.Instance.IncrementScore(1);
            }
            else
            {
                Debug.LogError("ScoreTrigger: GameManager.Instance is null!");
            }
            // Optional: Destroy this trigger object or its collider after triggering
            // Destroy(gameObject); // Or GetComponent<Collider2D>().enabled = false;
        }
    }
}
