using UnityEngine;
public class UIButtons : MonoBehaviour {
    public void ShowAchievements() {
        if (PlayGamesManager.Instance != null) {
            PlayGamesManager.Instance.ShowAchievements();
        }
    }

    public void ShowLeaderboard() {
        if (PlayGamesManager.Instance != null) {
            PlayGamesManager.Instance.ShowLeaderboard();
        }
    }
}
