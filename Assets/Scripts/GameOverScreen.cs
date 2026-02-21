using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour {
    public static GameOverScreen Instance; // single instance.

    // Text fields to display stats.
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TMP_Text currentScoreText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text timeText;

    private bool gameOver = false; // flag to signal that the game has ended.

    private void Awake() {
        // Enforce a single instance.
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        gameObject.SetActive(false); // ensure the panel is hidden.
    }

    // Read and display gameplay stats to the player when the game is over.
    public void EndGame(bool victory) {
        // Prevent multiple triggers.
        if (gameOver) {
            return;
        }

        gameOver = true; // flag the game as over.

        // Read the current score and time.
        int score = ScoreManager.Instance.GetScore();
        float time = Timer.Instance.GetElapsedTime();

        // Save the high score if achieved and read it back.
        SaveSystem.TrySetHighScore(score);
        int highScore = SaveSystem.LoadHighScore();

        // Stop the timer and freeze gameplay.
        Timer.Instance.StopTimer();
        Time.timeScale = 0.0f;

        // Update the UI.
        if (victory) {
            resultText.text = "Victory!";
        } else {
            resultText.text = "Game Over!";
        }

        currentScoreText.text = "Current Score: " + score;
        highScoreText.text = "High Score: " + highScore;
        timeText.text = "Time: " + FormatTime(time);

        // Show the panel.
        gameObject.SetActive(true);
    }

    // Restarts the game after a gameover.
    public void Restart() {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Format the time as mins:seconds
    private string FormatTime(float seconds) {
        int mins = Mathf.FloorToInt(seconds / 60f);
        int secs = Mathf.FloorToInt(seconds % 60f);
        return mins.ToString("00") + ":" + secs.ToString("00");
    }
}
