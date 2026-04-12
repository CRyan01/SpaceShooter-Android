using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class PlayGamesManager : MonoBehaviour {
    public static PlayGamesManager Instance; // A single instance.

    private bool isSignedIn = false; // true if the user signed in.

    // Flags unlocked achievements.
    private bool firstKillUnlocked = false;
    private bool triggerHappyUnlocked = false;
    private bool survivorUnlocked = false;
    private bool scoreHunterUnlocked = false;
    private bool collectorUnlocked = false;
    private bool sharpshooterUnlocked = false;

    private void Awake() {
        // Enforce a single instance.
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializePlayGames(); // Setup Play Games.
        } else {
            Destroy(gameObject);
        }
    }

    private void InitializePlayGames() {
        // Enable debug and activate.
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        // Try to authenticate.
        Social.localUser.Authenticate(success => {
            isSignedIn = success;
            Debug.Log("Play Games sign in: " + success);
        });
    }

    public void SubmitScore(long score) {
        // Return if not signed in.
        if (!isSignedIn) {
            return;
        }

        // Try to submit score.
        Social.ReportScore(score, GPGSIds.leaderboard_high_score, success => {
            Debug.Log("Score submitted: " + success);
        });
    }

    // Unlock the first kill achievement.
    public void UnlockFirstKill() {
        // Return if the achievement was already unlocked.
        if (firstKillUnlocked) {
            return;
        }
        Debug.Log("Playgamesmanager: unlockfirstkill.");

        firstKillUnlocked = true; // flag as unlocked.
        UnlockAchievement(GPGSIds.achievement_first_kill);
    }

    // Unlock the trigger happy achievement.
    public void UnlockTriggerHappy() {
        // Return if the achievement was already unlocked.
        if (triggerHappyUnlocked) {
            return;
        }

        triggerHappyUnlocked = true; // flag as unlocked.
        UnlockAchievement(GPGSIds.achievement_trigger_happy);
    }

    // Unlock the survivor achievement.
    public void UnlockSurvivor() {
        // Return if the achievement was already unlocked.
        if (survivorUnlocked) {
            return;
        }

        survivorUnlocked = true; // flag as unlocked.
        UnlockAchievement(GPGSIds.achievement_survivor);
    }

    // Unlock the score hunter achievement.
    public void UnlockScoreHunter() {
        // Return if the achievement was already unlocked.
        if (scoreHunterUnlocked) {
            return;
        }

        scoreHunterUnlocked = true; // flag as unlocked.
        UnlockAchievement(GPGSIds.achievement_score_hunter);
    }


    // Unlock the collector achievement.
    public void UnlockCollector() {
        // Return if the achievement was already unlocked.
        if (collectorUnlocked) {
            return;
        }

        collectorUnlocked = true; // flag as unlocked.
        UnlockAchievement(GPGSIds.achievement_collector);
    }

    // Unlock the sharpshooter achievement.
    public void UnlockSharpshooter() {
        // Return if the achievement was already unlocked.
        if (sharpshooterUnlocked) {
            return;
        }

        sharpshooterUnlocked = true; // flag as unlocked.
        UnlockAchievement(GPGSIds.achievement_sharpshooter);
    }

    public void UnlockAchievement(string achievementId) {
        // Return if not signed in.
        if (!isSignedIn) {
            return;
        }

        // Try to unlock the achievement that matches the passed in Id.
        Social.ReportProgress(achievementId, 100.0f, success => {
            Debug.Log("Achievement unlocked: " + achievementId + " success: " + success);
        });
    }

    public void ShowLeaderboard() {
        // Return if not signed in.
        if (!isSignedIn) {
            return;
        }

        Social.ShowLeaderboardUI();
    }

    public void ShowAchievements() {
        // Return if not signed in.
        if (!isSignedIn) {
            return;
        }

        Social.ShowAchievementsUI();
    }
}
