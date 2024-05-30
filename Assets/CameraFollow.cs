using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0f, 0f, -10f);
    public Transform timerText;
    public Transform scoreText;
    public Transform backLogo;

    private void LateUpdate()
    {
        if (player != null)
        {
            // Update camera position
            transform.position = player.position + offset;

            // Update timer and score text positions relative to the player
            if (timerText != null)
            {
                Vector3 timerPosition = player.position + offset + new Vector3(-2f, 3.5f, 0f);
                timerPosition.z = 0; // Ensure Z position stays at 0
                timerText.position = timerPosition;
            }

            if (scoreText != null)
            {
                Vector3 scorePosition = player.position + offset + new Vector3(2.5f, 3.5f, 0f);
                scorePosition.z = 0; // Ensure Z position stays at 0
                scoreText.position = scorePosition;
            }

            if (backLogo != null)
            {
                Vector3 logoPosition = player.position + offset + new Vector3(2.5f, 3.5f, 0f);
                logoPosition.z = 0; // Ensure Z position stays at 0
                backLogo.position = logoPosition;
            }
        }
    }
}
