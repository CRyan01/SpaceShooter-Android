using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    [SerializeField] private int maxHealth = 3; // the starting health of the player.
    private int currentHealth; // the players current health.

    private void Awake() {
        // Set current health with inspector value.
        currentHealth = maxHealth;
    }

    // When the player takes a hit reduce health by a set amount.
    public void Hit(int damage) {
        currentHealth -= damage;

        // Destroy the player if health goes below 0.
        if (currentHealth < 0) {
            Destroy(gameObject);
        }
    }
}
