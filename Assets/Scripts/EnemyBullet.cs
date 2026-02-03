using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    public float speed = 8.0f; // the bullets speed.
    public float lifetime = 1.0f; // time in seconds before the bullet is destroyed.
    public int damage = 1; // the bullets damage.

    void Start() {
        Destroy(gameObject, lifetime);
    }

    void Update() {
        // Move the bullet downwards at a given speed.
        transform.position -= Vector3.up * speed * Time.deltaTime;
    }

    // When a bullet collides with the player, reduce the players health
    // and destroy the bullet after.
    private void OnTriggerEnter2D(Collider2D other) {
        PlayerHealth player = other.GetComponent<PlayerHealth>();

        if (player != null) {
            player.Hit(damage);
            Destroy(gameObject);
        }
    }
}
