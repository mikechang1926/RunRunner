using UnityEngine;

public class CapsuleMovement : MonoBehaviour
{
    [Header("Horizontal Movement")]
    public float horizontalSpeed = 10f;
    public float horizontalLimit = 4f;

    private bool gameOver = false;
    private GameManager gameManager;

    void Start()
    {
        // Ensure GameManager is correctly found
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (gameOver)
            return;

        float horizontalInput = Input.GetAxis("Horizontal");
        float newXPosition = transform.position.x + horizontalInput * horizontalSpeed * Time.deltaTime;
        newXPosition = Mathf.Clamp(newXPosition, -horizontalLimit, horizontalLimit);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && !gameOver)
        {
            gameOver = true;
            gameManager.GameOver(); // explicitly call GameOver here
        }
    }
}
