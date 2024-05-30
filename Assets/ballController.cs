using UnityEngine;

public class BallController : MonoBehaviour
{
    public int maxGenerations = 3; // Maximum number of generations for ball splitting
    public GameObject ballPrefab; // Reference to the original ball prefab
    public float generationInterval = 15f; // Time between ball generations in seconds
    public GameObject smallerBallPrefab; // Reference to the smaller ball prefab
    public GameObject smallestBallPrefab; // Reference to the smallest ball prefab
    public float smallerBallSpeed = 5f; // Speed of the smaller balls

    public int maxDivisions = 1; // Maximum number of times the ball can be divided
    private int currentDivisions = 0; // Counter for the current number of divisions

    private float nextGenerationTime;
    private bool canSplit = true;
    private float splitCooldownTimer = 0f; // Timer for split cooldown
    public float splitCooldownDuration = 0.5f; // Cooldown time in seconds

    private void Start()
    {
        nextGenerationTime = Time.time + generationInterval;
        Debug.Log("BallController started. Next generation time: " + nextGenerationTime);
    }

    private void Update()
    {
        if (maxGenerations > 0 && Time.time >= nextGenerationTime)
        {
            GenerateBall();
            nextGenerationTime += generationInterval;
        }

        // Update split cooldown timer
        if (!canSplit)
        {
            splitCooldownTimer -= Time.deltaTime;
            if (splitCooldownTimer <= 0f)
            {
                canSplit = true;
                Debug.Log("Split cooldown ended. Can split again.");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Beam"))
        {
            Debug.Log("Beam collision detected.");
            if (currentDivisions < maxDivisions)
            {
                Debug.Log("Current divisions: " + currentDivisions + ", max divisions: " + maxDivisions);
                if (canSplit)
                {
                    SplitBall();
                    canSplit = false;
                    splitCooldownTimer = splitCooldownDuration;
                    Debug.Log("Split cooldown started.");
                }
                else
                {
                    Debug.Log("Cannot split: Cooldown active.");
                }
            }
            else
            {
                Debug.Log("Cannot split: Maximum divisions reached.");
            }
        }
    }

    private void GenerateBall()
    {
        if (maxGenerations > 0)
        {
            GameObject newBall = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            maxGenerations--;
            Debug.Log("New ball generated. Remaining generations: " + maxGenerations);
        }
    }

    private void SplitBall()
    {
        currentDivisions++;
        Debug.Log("Ball split. Current divisions: " + currentDivisions);

        // Spawn smaller balls
        for (int i = 0; i < 2; i++)
        {
            GameObject smallerBall = Instantiate(smallerBallPrefab, transform.position, Quaternion.identity);
            Rigidbody2D smallerBallRigidbody = smallerBall.GetComponent<Rigidbody2D>();
            if (smallerBallRigidbody != null)
            {
                Vector2 forceDirection = (i % 2 == 0) ? Vector2.left : Vector2.right;
                smallerBallRigidbody.velocity = forceDirection * smallerBallSpeed;
                Debug.Log("Smaller ball spawned and given velocity: " + smallerBallRigidbody.velocity);
            }
        }
        // Destroy the original ball
        Destroy(gameObject);
    }

    public int GetCurrentDivisions()
    {
        return currentDivisions;
    }

    public void IncrementDivisions()
    {
        currentDivisions++;
    }
}
