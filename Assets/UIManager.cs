using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Button backButton;
    public Text timerText;
    public Text scoreText;

    private float timer;
    private int score;

    public float totalTime = 60f; // Total time in seconds
    private float timeRemaining; // Remaining time in seconds
    public int winningScore = 150; // Score needed to win

    private void Start()
    {
        // Initialize the timer and score
        timer = 0f;
        score = 0;
        timeRemaining = totalTime;

        // Add listener to the back button
        if (backButton != null)
        {
            Debug.Log("Back button assigned.");
            // Add listener to the back button
            backButton.onClick.AddListener(OnBackButtonClicked);
            Debug.Log("Back button listener added.");
        }
        else
        {
            Debug.LogError("Back button is not assigned in the Inspector!");
        }
    }

    private void Update()
    {
        // Update the timer
        timeRemaining -= Time.deltaTime;
        UpdateTimerDisplay();

        // Update the score text (assuming the score can change over time)
        scoreText.text = "Score: " + score.ToString();

        // Check if the score has reached the winning threshold
        if (score >= winningScore)
        {
            Debug.Log("Score threshold reached. Loading YouWonScene.");
            // Load the "You Won" scene
            SceneManager.LoadScene("YouWonScene");
        }

        // Check if time has run out
        if (timeRemaining <= 0f)
        {
            Debug.Log("Time has run out. Loading YouLostScene.");
            // Load the "You Lost" scene when time runs out
            SceneManager.LoadScene("YouLostScene");
        }
    }

    private void UpdateTimerDisplay()
    {
        // Update the text to display the remaining time formatted as minutes:seconds
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void OnBackButtonClicked()
    {
        SceneManager.LoadScene("MainMenu"); // Replace "MainMenu" with your main menu scene name
    }

    // Method to update the score from other scripts
    public void UpdateScore(int newScore)
    {
        score = newScore;
    }

    // Method to add points to the score
    public void AddScore(int points)
    {
        score += points;
        Debug.Log($"Score updated: {score}");
    }
}
