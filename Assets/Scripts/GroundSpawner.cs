using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundPrefab;      // Ground prefab
    public float spawnInterval = 1f;     // Interval between ground spawns
    public float spawnDistance = 30f;    // Distance ahead where ground spawns
    private float timer;

    void Start()
    {
        // Initially spawn a few ground tiles
        for (int i = 0; i < 5; i++)
        {
            SpawnGround(i * 10f);  // Adjust 10f if your tile length is different
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= spawnInterval)
        {
            SpawnGround(spawnDistance);
            timer = 0f;
        }
    }

    void SpawnGround(float zPos)
    {
        Vector3 spawnPos = new Vector3(0f, 0f, zPos);
        Instantiate(groundPrefab, spawnPos, Quaternion.identity);
    }
}
