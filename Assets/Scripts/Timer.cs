using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour {
    // Create a singleton instance so other scripts can read the timer.
    public static Timer Instance;

    [SerializeField] private bool isRunning = true; // start running.
    [SerializeField] private float elapsedTime = 0.0f; // start at 0.
    [SerializeField] private TMP_Text timerText; // text to display the timer.

    // Returns elapsed time.
    public float GetElapsedTime() {
        return elapsedTime;
    }

    // Returns wether the timer is running or not.
    public bool GetIsRunning() {
        return isRunning;
    }

    private void Awake() {
        // Enforce only one instance.
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnDestroy() {
        // Reset instance on reload.
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Update() {
        // Do nothing if the timer isn't running.
        if (!isRunning) {
            return;
        }

        // Otherwise increment the timer.
        elapsedTime += Time.deltaTime;

        // Update the timer text with formatted time elapsed.
        if (timerText != null) {
            timerText.text = FormatTime(elapsedTime);
        }
    }

    // Starts the timer.
    public void StartTimer() {
        isRunning = true;
    }

    // Stops the timer.
    public void StopTimer() {
        isRunning = false;
    }

    // Resets the timer.
    public void ResetTimer() {
        elapsedTime = 0.0f;
    }

    // Returns true if the timer has reached a certain time.
    public bool HasReached(float seconds) {
        if (elapsedTime >= seconds) {
            return true;
        }
        return false;
    }

    // Formats seconds into mins:secs.
    private string FormatTime(float seconds) {
        int mins = Mathf.FloorToInt(seconds / 60.0f);
        int secs = Mathf.FloorToInt(seconds % 60.0f);
        string formattedTime = mins.ToString("00") + ":" + secs.ToString("00");
        return formattedTime;
    }
}
