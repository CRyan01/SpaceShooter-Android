using UnityEngine;

public class Pickup : MonoBehaviour {
    // Enum type for three different pickups, a shield, heal, and fire rate boost.
    public enum PickupType {
        Shield,
        FireRate,
        Heal
    }

    public PickupType type; // to store the type of pickup.

    public float fallSpeed = 2.5f; // pickup fall speed.

    // Pickup effect values.
    public int healtAmount = 1;
    public int shieldAmount = 1;
    public float fireRateAdd = 3.0f;
    public float fireRateDuration = 6.0f;

    private void Update() {
        // Move the pickup downwards.
        transform.position += Vector3.left * fallSpeed * Time.deltaTime;
    }

    // When the pickup collides with another object.
    private void OnTriggerEnter2D(Collider2D other) {
        // Store refs to the players scripts.
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        PlayerController playerController = other.GetComponent<PlayerController>();

        // Ignore collisions with non-player objects.
        if (playerHealth == null && playerController == null) {
            return;
        }

        if (type == PickupType.Shield) {
            // When the player picks up a shield.
            if (playerHealth != null) {
                // Add the shield to the players health.
                playerHealth.AddShield(shieldAmount);
            }
        } else if (type == PickupType.FireRate) {
            // When the player picks up a fire rate boost.
            if (playerController != null) {
                // Adjust the players fire rate by a specific amount, for a set duration.
                playerController.AddFireRate(fireRateAdd, fireRateDuration);
            }
        } else if (type == PickupType.Heal) {
            // When the player picks up a heal.
            if (playerHealth != null) {
                // Heal the player by a set amount.
                playerHealth.Heal(healtAmount);
            }
        }

        Destroy(gameObject); // Destroy the pickup.
    }
}
