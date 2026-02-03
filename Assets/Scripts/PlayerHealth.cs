using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    [SerializeField] private int maxHealth = 3; // the starting health of the enemy.
    private int currentHealth; // the enemies current health.

    private void Awake() {
        // Set current health with inspector value.
        currentHealth = maxHealth;
    }

    // When the enemy takes a hit reduce health by a set amount.
    public void Hit(int damage) {
        currentHealth -= damage;

        // Destroy the enemy if health goes below 0.
        if (currentHealth < 0) {
            Destroy(gameObject);
        }
    }
}
