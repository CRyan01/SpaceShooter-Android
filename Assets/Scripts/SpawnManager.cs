using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour {
    public GameObject[] tier1Enemies; // spawn from 0s.
    public GameObject[] tier2Enemies; // spawn from 60s.
    public GameObject[] tier3Enemies; // spawn from 120s.

    public GameObject miniBoss1Prefab; // spawn at 60s.
    public GameObject miniBoss2Prefab; // spawn at 120s.
    public GameObject mainBossPrefab; // spawn at 180s.

    public float startSpawnInterval = 0.9f; // early game.
    public float endSpawnInterval = 0.55f; // late game.
    public float duration = 180.0f; // seconds to reach the end spawn interval.

    public float xPadding = 0.7f; // to prevent clipping.
    public float yOffset = 1.5f; // to spawn outside of view.

    // Stop normal spawns when fighting the last boss.
    public bool stopNormalSpawnsAfterMainBoss = true;

    // Boss state flags.
    private bool miniBoss1Spawned = false;
    private bool miniBoss2Spawned = false;
    private bool mainBossSpawned = false;

    private bool isBossActive = false;
    private GameObject activeBoss = null;

    private Camera camera; // a ref to the main camera to get bounds.
    private float nextSpawnTime; // time until the next spawn.

    public GameObject[] pickupPrefabs; // an array of pickups to spawn.
    public float pickupSpawnInterval = 10.0f; // how often to try to spawn a pickup.
    public float pickupSpawnChance = 0.7f; // the chance of a pickup spawning when triggered.

    private float nextPickupSpawnTime; // time until the next pickup spawns.

    private void Start() {
        // Get a ref to the camera to get viewport bounds.
        camera = Camera.main;
    }

    private void Update() {
        // Ensure there is a valid camera reference.
        if (camera == null) {
            return;
        }
        // Ensure there is a timer instance.
        if (Timer.Instance == null) {
            return;
        }

        // Get elapsed time from the timer.
        float elapsedTime = Timer.Instance.GetElapsedTime();

        // If a boss was active, but is now destroyed, clear the active boss state.
        if (isBossActive && activeBoss == null) {
            isBossActive = false;
        }

        // Trigger bosses at fixed times.
        TrySpawnBosses(elapsedTime);

        // When the main boss appears, stop normal enemy spawns forever.
        if (stopNormalSpawnsAfterMainBoss && mainBossSpawned) {
            return;
        }

        // Pause normal enemies while a boss is active.
        if (isBossActive) {
            return;
        }

        // Calculate the current spawn interval.
        float currentSpawnInterval = CalculateSpawnInterval(elapsedTime);

        // Check if its time to spawn a new enemy.
        if (Time.time >= nextSpawnTime) {
            // If it is, spawn an enemy, and update the time until the next spawn.
            SpawnEnemyByTime(elapsedTime);
            nextSpawnTime = Time.time + currentSpawnInterval;
        }

        // Try to spawn a pickup when its time and theres no active boss.
        if (!isBossActive && Time.time >= nextPickupSpawnTime) {
            TrySpawnPickup();
            nextPickupSpawnTime = Time.time + currentSpawnInterval;
        }
    }


    // Try to spawn bosses at fixed times.
    private void TrySpawnBosses(float elapsedTime) {
        // Mini boss at 60s
        if (!miniBoss1Spawned && elapsedTime >= 60.0f) {
            SpawnBoss(miniBoss1Prefab);
            miniBoss1Spawned = true;
            return;
        }

        // Mini boss 2 at 120s
        if (!miniBoss2Spawned && elapsedTime >= 120.0f) {
            SpawnBoss(miniBoss2Prefab);
            miniBoss2Spawned = true;
            return;
        }

        // Final boss at 180s
        if (!mainBossSpawned && elapsedTime >= 180.0f) {
            SpawnBoss(mainBossPrefab);
            mainBossSpawned = true;
            return;
        }
    }

    // Spawn a specific boss.
    private void SpawnBoss(GameObject bossPrefab) {
        // Check for a valid prefab.
        if (bossPrefab == null) {
            return;
        }

        float centerX = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.0f, 0.0f)).x;
        float topY = camera.ViewportToWorldPoint(new Vector3(0.5f, 1.0f, 0.0f)).y + yOffset;

        GameObject boss = Instantiate(bossPrefab, new Vector3(centerX, topY, 0.0f), Quaternion.identity);

        activeBoss = boss;
        isBossActive = true;
    }

    // Try to spawn a pickup
    private void TrySpawnPickup() {
        // Ensure there are valid prefabs in the array.
        if (pickupPrefabs == null || pickupPrefabs.Length == 0) {
            return;
        }

        float roll = Random.value;

        if (roll > pickupSpawnChance) {
            return;
        }

        SpawnPickup();
    }

    // Spawn a pickup
    private void SpawnPickup() {
        // Get world bounds from the viewport.
        float leftX = camera.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f)).x + xPadding;
        float rightX = camera.ViewportToWorldPoint(new Vector3(1.0f, 0.0f, 0.0f)).x - xPadding;
        float topY = camera.ViewportToWorldPoint(new Vector3(0.5f, 1.0f, 0.0f)).y + yOffset;

        // Generate a random x position to vary spawns.
        float randomX = Random.Range(leftX, rightX);

        // Select a random pickup to spawn from the array.
        GameObject pickupPrefab = pickupPrefabs[Random.Range(0, pickupPrefabs.Length)];

        // Create the selected pickup.
        Instantiate(pickupPrefab, new Vector3(randomX, topY, 0.0f), Quaternion.identity);
    }

    // Calculates spawn interval based on elapsed time, to increase difficulty.
    private float CalculateSpawnInterval(float elapsedTime) {
        float time = Mathf.Clamp01(elapsedTime / duration);
        float currentInterval = Mathf.Lerp(startSpawnInterval, endSpawnInterval, time);
        return currentInterval;
    }

    public void SpawnEnemyByTime(float elapsedTime) {
        // Build the spawn pool based on elapsed time.
        List<GameObject> spawnPool = BuildSpawnPool(elapsedTime);

        // Ensure there are enemies in the spawn pool.
        if (spawnPool.Count == 0) {
            return;
        }

        // Get world bounds from the viewport.
        float leftX = camera.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f)).x + xPadding;
        float rightX = camera.ViewportToWorldPoint(new Vector3(1.0f, 0.0f, 0.0f)).x - xPadding;
        float topY = camera.ViewportToWorldPoint(new Vector3(0.5f, 1.0f, 0.0f)).y + yOffset;

        // Generate a random x position to vary spawns.
        float randomX = Random.Range(leftX, rightX);

        // Select a random enemy from the spawn pool to spawn.
        GameObject enemyPrefab = spawnPool[Random.Range(0, spawnPool.Count)];

        // Create the selected enemy.
        Instantiate(enemyPrefab, new Vector3(randomX, topY, 0.0f), Quaternion.identity);

    }

    // Build the spawn pool by adding tiered arrays to the list of possible spawns
    // based on time elapsed.
    private List<GameObject> BuildSpawnPool(float elapsedTime) {
        List<GameObject> spawnPool = new List<GameObject>();

        // Add tier 1 enemies by default.
        AddArrayToSpawnPool(tier1Enemies, spawnPool);

        // When 60 seconds pass add tier 2 enemies.
        if (elapsedTime >= 60.0f) {
            AddArrayToSpawnPool(tier2Enemies, spawnPool);
        }

        // When 120 seconds pass add tier 3 enemies.
        if (elapsedTime >= 120.0f) {
            AddArrayToSpawnPool(tier3Enemies, spawnPool);
        }

        return spawnPool;
    }

    // Add enemies from arrays to the list of possible spawns.
    private void AddArrayToSpawnPool(GameObject[] enemyArray, List<GameObject> spawnPool) {
        // Ensure the array and list are valid.
        if (spawnPool == null || enemyArray == null || enemyArray.Length == 0) {
            return;
        }

        // Iterate through the array of enemies.
        for (int i = 0; i < enemyArray.Length; i++) {
            // Check if the index is not null.
            if (enemyArray[i] != null) {
                // If not add the enemy to the list of spawns.
                spawnPool.Add(enemyArray[i]);
            }
        }
    }
}
