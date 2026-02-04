using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject[] enemyPrefabs; // an array of enemy prefabs.
    public float spawnInterval = 0.8f; // the time between new spawns.
    public float xPadding = 0.7f; // padding.

    private Camera camera; // a ref to the camera.
    private float nextSpawnTime; // time until the next spawn.

    void Start() {
        camera = Camera.main; // get a ref to the main camera.
    }

    void Update() {
        // Check if its time to spawn a new enemy. If it is spawn an enemy
        // and calculate the next spawn time.
        if (Time.time >= nextSpawnTime) {
            Spawn();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    public void Spawn() {
        // Check for a valid array of enemy prefabs, return if theres a problem.
        if (enemyPrefabs == null || enemyPrefabs.Length == 0) {
            return;
        }

        // Get bounds from the viewport.
        float leftX = camera.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f)).x;
        float rightX = camera.ViewportToWorldPoint(new Vector3(1.0f, 0.0f, 0.0f)).x;
        float topY = camera.ViewportToWorldPoint(new Vector3(0.5f, 1.0f, 0.0f)).y + 1.5f;

        float randomX = Random.Range(leftX, rightX); // get a random x pos.

        // Spawn a new enemy.
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Instantiate(enemyPrefab, new Vector3(randomX, topY, 0.0f), Quaternion.identity);
    }
}
