using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    public float speed = 18.0f; // the bullets speed.
    public int damage = 1; // the bullets damage.

    void Update() {
        // Move the bullet upwards at a given speed.
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    // When a bullet collides with an enemy, reduce the enemies health
    // and destroy the bullet after.
    private void OnTriggerEnter2D(Collider2D other) {
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();

        if (enemy != null) {
            enemy.Hit(damage);
            Destroy(gameObject);
        }
    }
}
