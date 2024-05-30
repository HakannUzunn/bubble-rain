using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeTransition : MonoBehaviour
{
    public float fadeDuration = 2f; // Duration of the fade transition
    public string nextSceneName; // Name of the next scene to load

    private Image blackOverlay;

    private void Start()
    {
        blackOverlay = GetComponent<Image>();
        StartCoroutine(FadeOutAndLoadNextScene());
    }

    private IEnumerator FadeOutAndLoadNextScene()
    {
        float elapsedTime = 0f;
        Color startColor = blackOverlay.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f); // Fully opaque black

        while (elapsedTime < fadeDuration)
        {
            // Calculate the current alpha value based on the elapsed time
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            blackOverlay.color = Color.Lerp(startColor, targetColor, alpha);

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the overlay is fully opaque
        blackOverlay.color = targetColor;

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
