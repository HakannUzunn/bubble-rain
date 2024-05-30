using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Transform playerTransform;
    public float parallaxSpeed = 0.5f; // Adjust this value to control the parallax effect

    private Vector3 lastPlayerPosition;

    private void Start()
    {
        lastPlayerPosition = playerTransform.position;
    }

    private void Update()
    {
        float deltaMovement = playerTransform.position.x - lastPlayerPosition.x;

        // Move each background layer based on the player's movement
        foreach (Transform layer in transform)
        {
            float parallaxAmount = deltaMovement * parallaxSpeed * layer.position.z;
            layer.position += new Vector3(parallaxAmount, 0, 0);
        }

        lastPlayerPosition = playerTransform.position;
    }
}
