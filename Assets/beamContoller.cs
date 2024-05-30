using UnityEngine;

public class BeamController : MonoBehaviour
{
    public GameObject smallerBallPrefab; // Reference to the smaller ball prefab
    public GameObject smallestBallPrefab; // Reference to the smallest ball prefab
    public float smallerBallSpeed = 5f; // Speed of the smaller balls
    public float smallestBallSpeed = 5f; // Speed of the smallest balls
    public GameObject beamPrefab;  // Reference to the beam prefab
    public Transform firePoint;    // The point from which the beam is fired
    public float beamSpeed = 5f;   // Speed at which the beam travels
    public float beamLifetime = 2f; // Lifetime of the beam

    private ScoreManager scoreManager; // Reference to the ScoreManager

    private void Start()
    {
        // Use the new method to find the ScoreManager
        scoreManager = Object.FindFirstObjectByType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager not found in the scene!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireBeam();
        }
    }

    public void FireBeam()
    {
        GameObject beam = Instantiate(beamPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = beam.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(0f, beamSpeed);
        }
        Destroy(beam, beamLifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Beam triggered with: " + other.gameObject.name);
        if (other.CompareTag("Ball"))
        {
            SplitBall(other.gameObject);
            scoreManager.IncreaseScore(10);
        }
        else if (other.CompareTag("SmallerBall"))
        {
            SplitSmallerBall(other.gameObject);
            scoreManager.IncreaseScore(10);
        }
        else if (other.CompareTag("SmallestBall"))
        {
            DestroySmallestBall(other.gameObject);
            scoreManager.IncreaseScore(10);
        }
    }

    private void SplitBall(GameObject ball)
    {
        BallController ballController = ball.GetComponent<BallController>();
        if (ballController != null && ballController.GetCurrentDivisions() < ballController.maxDivisions)
        {
            ballController.IncrementDivisions();
            for (int i = 0; i < 2; i++)
            {
                GameObject smallerBall = Instantiate(smallerBallPrefab, ball.transform.position, Quaternion.identity);
                smallerBall.tag = "SmallerBall";
                Rigidbody2D smallerBallRigidbody = smallerBall.GetComponent<Rigidbody2D>();
                if (smallerBallRigidbody != null)
                {
                    Vector2 forceDirection = (i % 2 == 0) ? Vector2.left : Vector2.right;
                    smallerBallRigidbody.velocity = forceDirection * smallerBallSpeed;
                    Debug.Log("Smaller ball spawned from beam split and given velocity: " + smallerBallRigidbody.velocity);
                }
            }
            Destroy(ball);
        }
    }

    private void SplitSmallerBall(GameObject smallerBall)
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject smallestBall = Instantiate(smallestBallPrefab, smallerBall.transform.position, Quaternion.identity);
            smallestBall.tag = "SmallestBall";
            Rigidbody2D smallestBallRigidbody = smallestBall.GetComponent<Rigidbody2D>();
            if (smallestBallRigidbody != null)
            {
                Vector2 forceDirection = (i % 2 == 0) ? Vector2.left : Vector2.right;
                smallestBallRigidbody.velocity = forceDirection * smallestBallSpeed;
                Debug.Log("Smallest ball spawned from beam split and given velocity: " + smallestBallRigidbody.velocity);
            }
        }
        Destroy(smallerBall);
    }

    private void DestroySmallestBall(GameObject smallestBall)
    {
        Destroy(smallestBall);
    }
}
