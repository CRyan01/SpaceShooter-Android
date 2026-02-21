using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour {
    public static ScoreManager Instance;

    // Variables to store score and text to display it.
    [SerializeField] private TMP_Text scoreText;
    private int score = 0;

    private void Awake() {
        // Enforce a single instance.
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        UpdateUI(); // update the UI with score.
    }

    // Adds a given amount of score.
    public void AddScore(int amount) {
        score += amount;
        if (score < 0) {
            score = 0;
        }

        UpdateUI();
    }

    // Returns the current score.
    public int GetScore() {
        return score;
    }

    // Updates the UI text with the current score.
    private void UpdateUI() {
        if (scoreText != null) {
            scoreText.text = "Score: " + score;
        }
    }
}
