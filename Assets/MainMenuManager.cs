using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button playButton;
    public Button optionsButton;
    public Button quitButton;

    private void Start()
    {
        // Add listener to the play button
        if (playButton != null)
        {
            playButton.onClick.AddListener(OnPlayButtonClicked);
        }
        
        // Add listeners for the other buttons if needed in the future
        if (optionsButton != null)
        {
            optionsButton.onClick.AddListener(OnOptionsButtonClicked);
        }
        
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(OnQuitButtonClicked);
        }
    }

    private void OnPlayButtonClicked()
    {
        // Load the SampleScene
        SceneManager.LoadScene("SampleScene");
    }

    private void OnOptionsButtonClicked()
    {
        // Placeholder for options button functionality
        Debug.Log("Options button clicked");
    }

    private void OnQuitButtonClicked()
    {
        // Placeholder for quit button functionality
        Debug.Log("Quit button clicked");
        Application.Quit();
    }
}
