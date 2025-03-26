using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TMP_Text pointsText;
    public GameObject gameOverPanel;
    private float points = 0f;
    private bool gameOver = false;

    void Start()
    {
        gameOverPanel.SetActive(false);
        MoveBackward.canMove = true;
    }

    void Update()
    {
        if (gameOver)
            return;  // explicitly stop counting points once gameOver is true

        points += Time.deltaTime * MoveBackward.obstacleSpeed;
        pointsText.text = $"Points: {Mathf.FloorToInt(points)}";
    }

    public void GameOver()
    {
        if (gameOver)
            return;

        gameOver = true;
        MoveBackward.canMove = false;
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
