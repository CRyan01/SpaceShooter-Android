using UnityEngine;

public class EnemyAutofire : MonoBehaviour {
    public float fireRate = 2.0f; // how many bullets are fired a second.

    public Transform[] firePoints; // where enemy fired bullets spawn.
    public GameObject bulletPrefab; // the bullet object prefab.

    public float maxSpread = 4.0f; // max spread for shots.

    private float nextShotTime; // time when the next bullet can be fired.

    private void Update() {
        // Check for valid refs to the bullet prefab and fire points.
        if (bulletPrefab == null || firePoints == null || firePoints.Length == 0) {
            return;
        }

        // Do nothing if firerate is zero.
        if (fireRate <= 0.0f) {
            return;
        }

        // Check if enough time passed to fire another bullet.
        if (Time.time >= nextShotTime) {
            // Spawn a bullet at the firepoints.
            FireAllPoints();
            // Set the time for the next shot.
            nextShotTime = Time.time + (1.0f / fireRate);
        }
    }

    // Creates a bullet at a firepoint with some variance.
    private void FireAllPoints() {
        for (int i = 0; i < firePoints.Length; i++) {
            Transform firePoint = firePoints[i]; // select a firepoint.

            if (firePoint == null) {
                continue;
            }

            // Apply a slight offest to the shot.
            float shotOffset = Random.Range(-maxSpread, maxSpread);
            Quaternion rotation = firePoint.rotation * Quaternion.Euler(0.0f, 0.0f, shotOffset);

            // Create the bullet.
            Instantiate(bulletPrefab, firePoint.position, rotation);

            // Play a sound effect.
            AudioManager.Instance.EnemyFired();
        }
    }
}
