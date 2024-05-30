using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public ScoreManager scoreManager;
    public Text scoreText;

    private void Update()
    {
        scoreText.text = "Score: " + scoreManager.GetScore();
    }
}
