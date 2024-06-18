using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Variables to manage the game state
    public enum GameState { Menu, Playing, Paused, GameOver }
    public GameState CurrentState { get; private set; }

    // Variables to manage score, level, etc.
    public int Score { get; private set; }
    public int Level { get; private set; }

    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        // Initialize game variables
        Score = 0;
        Level = 1;
        CurrentState = GameState.Menu;
    }

    // Method to start the game
    public void StartGame()
    {
        CurrentState = GameState.Playing;
        // Additional logic to start the game (e.g., initialize game board)
    }

    // Method to pause the game
    public void PauseGame()
    {
        if (CurrentState == GameState.Playing)
        {
            CurrentState = GameState.Paused;
            // Additional logic to pause the game
        }
    }

    // Method to resume the game
    public void ResumeGame()
    {
        if (CurrentState == GameState.Paused)
        {
            CurrentState = GameState.Playing;
            // Additional logic to resume the game
        }
    }

    // Method to end the game
    public void GameOver()
    {
        CurrentState = GameState.GameOver;
        // Additional logic for game over (e.g., display game over screen)
    }

    // Method to reset the game
    public void ResetGame()
    {
        InitializeGame();
        // Additional logic to reset the game (e.g., clear game board)
    }

    // Method to increase the score
    public void IncreaseScore(int amount)
    {
        Score += amount;
        // Update the UI or other related components
    }

    // Method to advance to the next level
    public void NextLevel()
    {
        Level++;
        // Additional logic for level progression (e.g., increase difficulty)
    }
}
