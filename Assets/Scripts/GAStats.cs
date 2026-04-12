using UnityEngine;
using GameAnalyticsSDK;

public class GAStats {
    public static int shotsFired = 0; // the number of shots fired in a run.
    public static int enemiesKilled = 0; // the number of enemies killed in a run.
    public static int runsPlayed = 0; // the number of back to back runs.
    public static int pickupsCollected = 0; // the number of pickups collected in a run.
    public static int finalScore = 0; // the final score achieved.
    public static float survivalTime = 0.0f; // time survived.

    // Reset for a new run.
    public static void ResetRun() {
        shotsFired = 0;
        enemiesKilled = 0;
        pickupsCollected = 0;
        finalScore = 0;
        survivalTime = 0.0f;
    }

    // Send all stats.
    public static void SendRunStats() {
        GameAnalytics.NewDesignEvent("shots_fired", shotsFired);
        GameAnalytics.NewDesignEvent("enemies_killed", enemiesKilled);
        GameAnalytics.NewDesignEvent("final_score", finalScore);
        GameAnalytics.NewDesignEvent("survival_time", survivalTime);
        GameAnalytics.NewDesignEvent("pickups_collected", pickupsCollected);

        Debug.LogWarning("GAStats: Stats Sent");
    }

    // Increment shotsFired.
    public static void ShotFired() {
        shotsFired++;
    }

    // Update the number of killed enemies.
    public static void EnemyKilled() {
        enemiesKilled++;
    }

    // Update the number of pickups collected.
    public static void PickupCollected() {
        pickupsCollected++;
    }

    // Update the final score.
    public static void SetFinalScore(int score) {
        finalScore = score;
    }

    // Update survival time.
    public static void SetSurvivalTime(float time) {
        survivalTime = time;
    }

    // Update and send total consecutive runs.
    public static void RunStarted() {
        runsPlayed++;
        GameAnalytics.NewDesignEvent("runs_played", runsPlayed);
    }
}
