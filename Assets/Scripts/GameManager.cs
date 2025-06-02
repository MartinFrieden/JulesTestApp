using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class GameManager : MonoBehaviour
{
    public UnityEngine.UI.Text scoreText; // Assign in Inspector
    private int score = 0;
    public enum GameState { Start, Playing, GameOver }
    public static GameManager Instance { get; private set; }
    public GameState CurrentState { get; private set; }

    public GameObject bird; // Optional: Assign the bird GameObject for more direct control if needed

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Optional: if your GameManager needs to persist across scenes (not typical for simple restart)
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Initial game state
        CurrentState = GameState.Start;
        InitializeScore();
        Time.timeScale = 1; // Ensure time scale is normal at start, especially after a restart

        // Find the bird if not assigned (alternative to public field)
        if (bird == null) bird = GameObject.FindGameObjectWithTag("Player"); // Assuming bird is tagged "Player"

        if (bird != null)
        {
            BirdController birdController = bird.GetComponent<BirdController>();
            if (birdController != null && birdController.rb != null)
            {
                 birdController.rb.isKinematic = true; // Start with bird physics paused
            }
        }
        else
        {
            Debug.LogWarning("GameManager: Bird GameObject not found. Ensure bird is in scene and tagged 'Player' or assigned manually.");
        }

        Debug.Log("GameManager initialized. State: Start. Press Jump to play.");
        // Optionally, show a "Tap to Start" UI message here
    }

    void Update()
    {
        if (CurrentState == GameState.Start)
        {
            // Check for jump input (Space or Mouse Click)
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                StartGame();
            }
        }
        else if (CurrentState == GameState.GameOver)
        {
            // Check for restart input (e.g., R key or Mouse Click)
            if (Input.GetKeyDown(KeyCode.R) || Input.GetMouseButtonDown(0))
            {
                RestartGame();
            }
            // Optionally, show a "Game Over - Press R to Restart" UI message here
        }
    }

    public void StartGame()
    {
        InitializeScore(); // Reset score when game starts
        if (CurrentState == GameState.Start)
        {
            CurrentState = GameState.Playing;
            Time.timeScale = 1f; // Ensure game is running

            if (bird != null)
            {
                BirdController birdController = bird.GetComponent<BirdController>();
                if (birdController != null && birdController.rb != null)
                {
                    birdController.rb.isKinematic = false; // Enable bird physics
                    birdController.Jump(); // Give an initial jump to start
                }
            }
            Debug.Log("Game Started! State: Playing");
            // Optionally, hide "Tap to Start" UI, enable pipe spawner if it was disabled
        }
    }

    public void GameOver()
    {
        if (CurrentState == GameState.Playing)
        {
            CurrentState = GameState.GameOver;
            Time.timeScale = 0f; // Pause the game
            Debug.Log("Final Score: " + score);
            // Update UI one last time if needed, or show on a game over panel
            Debug.Log("Game Over! State: GameOver. Press R or Click to restart.");
            // Optionally, show "Game Over" UI and score
        }
    }

    public void RestartGame()
    {
        // Score will be reset when the new scene loads and GameManager.Start() is called.
        Time.timeScale = 1f; // IMPORTANT: Reset time scale before loading scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void InitializeScore()
    {
        score = 0;
        if (scoreText != null) scoreText.text = "Score: " + score.ToString();
        else Debug.LogWarning("ScoreText UI element not assigned in GameManager.");
    }

    public void IncrementScore(int amount)
    {
        if (CurrentState != GameState.Playing) return; // Only score while playing

        score += amount;
        if (scoreText != null) scoreText.text = "Score: " + score.ToString();
        Debug.Log("Score: " + score);
    }
}
