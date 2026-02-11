using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    public float moveSpeed = 3.0f; // the enemies movespeed.

    public float fireRate = 2.0f; // how many bullets are fired a second.

    public Transform firePoint; // where enemy fired bullets spawn.
    public GameObject bulletPrefab; // the bullet object prefab.

    float nextShotTime; // time when the next bullet can be fired.

    private void Update() {
        Move();
        AutoFire();
    }

    void Move() {
        // Move the enemy downwards.
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }

    // Automatically fire a bullet.
    void AutoFire() {
        // Check for valid refs to the bullet prefab and fire point.
        if (!bulletPrefab || !firePoint) {
            return;
        }

        // Check if enough time passed to fire another bullet.
        if (Time.time >= nextShotTime) {
            // Spawn a bullet at the firepoint.
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            // Set the time for the next shot.
            nextShotTime = Time.time + (1.0f / fireRate);
        }
    }
}
