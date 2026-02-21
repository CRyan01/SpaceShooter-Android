using TMPro;
using UnityEngine;

public class PauseManager : MonoBehaviour {
    private bool isPaused; // flags if the game is paused or not.

    public TMP_Text pauseButtonText;

    private void Start() {
        isPaused = false; // start unpaused.
    }

    public void TogglePause() {
        if (isPaused) {
            // If the game is paused unpause it.
            isPaused = false;
            pauseButtonText.text = "Pause";
            Time.timeScale = 1.0f;
        } else {
            // Otherwise pause it.
            isPaused = true;
            pauseButtonText.text = "Unpause";
            Time.timeScale = 0.0f;
        }
    }
}
