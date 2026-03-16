using UnityEngine;
using GameAnalyticsSDK;

public class GAStats {
    // Track the number of shots fired in a run.
    public static int shotsFired = 0;

    // Increment shotsFired by 1.
    public static void ShotFired() {
        shotsFired++;
    }

    // To send total shots fired at the end of a run.
    public static void SendShotsFired() {
        GameAnalytics.NewDesignEvent("stat:shots_fired", shotsFired);
    }

    // Resets shots fired for a new run.
    public static void ResetRun() {
        shotsFired = 0;
    }
}
