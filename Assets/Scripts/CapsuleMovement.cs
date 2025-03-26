using UnityEngine;

public class CapsuleMovement : MonoBehaviour
{
    [Header("Horizontal Movement")]
    public float horizontalSpeed = 10f;
    public float horizontalLimit = 4f;

    private bool gameOver = false;
    private GameManager gameManager;
    private Animator animator;  // Reference to Animator component

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        animator = GetComponent<Animator>();  // Get Animator component
    }

    void Update()
    {
        if (gameOver)
            return;  // Clearly stop all movement when game is over

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

            // Set Animator Boolean "blocked" to true, clearly triggering animation
            animator.SetBool("blocked", true);

            gameManager.GameOver();
        }
    }
}
