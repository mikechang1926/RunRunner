using UnityEngine;

public class MoveBackward : MonoBehaviour
{
    public static float obstacleSpeed = 10f;  // shared speed across obstacles
    public static bool canMove = true;        // global toggle for movement

    private float destroyZPos = -10f;

    void Update()
    {
        // Move only if allowed
        if (canMove)
        {
            transform.Translate(Vector3.back * obstacleSpeed * Time.deltaTime);
        }

        // Destroy obstacle once passed
        if (transform.position.z < destroyZPos)
            Destroy(gameObject);
    }
}
