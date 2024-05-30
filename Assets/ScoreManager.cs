using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;
    public static ScoreManager Instance { get { return instance; } }

    private int score = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to increase the score
    public void IncreaseScore(int points)
    {
        score += points;
        //Debug.Log("Score increased by " + points + ". Total score: " + score);
    }

    // Method to get the current score
    public int GetScore()
    {
        return score;
    }
}
