using UnityEngine;
using GameAnalyticsSDK;

public class GAInit : MonoBehaviour {
    private void Awake() {
        GameAnalytics.Initialize();
        Debug.Log("GameAnalytics.Initialize called.");
    }
}
