using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;

    [Header("Spawn Timing (Consistent spacing)")]
    public float spawnInterval = 2f; // consistent spacing between rows
    private float nextSpawnTime;
    private float difficultyTimer;

    [Header("Obstacle Density Settings")]
    public float spawnDistance = 30f;
    public float horizontalRange = 4f;
    public int initialObstaclesPerRow = 1;
    public int maxObstaclesPerRow = 5;
    private int currentObstaclesPerRow;

    [Header("Guaranteed Path Settings")]
    public float playerWidth = 1f; 
    public float minGapWidth = 1.5f;

    [Header("Obstacle Speed Increase")]
    public float initialObstacleSpeed = 10f;
    public float obstacleSpeedIncrease = 5f;  // speed increment each difficulty interval
    public float maxObstacleSpeed = 100f;

    void Start()
    {
        currentObstaclesPerRow = initialObstaclesPerRow;
        nextSpawnTime = Time.time + spawnInterval;

        // Set initial obstacle speed clearly
        MoveBackward.obstacleSpeed = initialObstacleSpeed;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnObstacleRowWithGap();
            nextSpawnTime = Time.time + spawnInterval;
        }

        difficultyTimer += Time.deltaTime;
        if (difficultyTimer >= 5f) // increase difficulty every 5 seconds
        {
            IncreaseDifficulty();
            difficultyTimer = 0f;
        }
    }

    void SpawnObstacleRowWithGap()
    {
        // Pick guaranteed gap position clearly
        float gapCenter = Random.Range(-horizontalRange + minGapWidth / 2f, horizontalRange - minGapWidth / 2f);

        int spawnedObstacles = 0;
        int attempts = 0;

        // Spawn obstacles while keeping guaranteed gap clear
        while (spawnedObstacles < currentObstaclesPerRow && attempts < currentObstaclesPerRow * 3)
        {
            float obstacleXPos = Random.Range(-horizontalRange, horizontalRange);

            // Ensure obstacle doesn't overlap guaranteed gap
            if (Mathf.Abs(obstacleXPos - gapCenter) < minGapWidth / 2f)
            {
                attempts++;
                continue; // skip placement if it interferes with gap
            }

            Vector3 spawnPosition = new Vector3(obstacleXPos, 0.5f, spawnDistance);
            Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);

            spawnedObstacles++;
        }
    }

    void IncreaseDifficulty()
    {
        // Gradually increase obstacles per row
        if (currentObstaclesPerRow < maxObstaclesPerRow)
        {
            currentObstaclesPerRow++;
        }

        // Clearly increase obstacle speed over time
        MoveBackward.obstacleSpeed += obstacleSpeedIncrease;
        MoveBackward.obstacleSpeed = Mathf.Clamp(MoveBackward.obstacleSpeed, initialObstacleSpeed, maxObstacleSpeed);
    }
}
