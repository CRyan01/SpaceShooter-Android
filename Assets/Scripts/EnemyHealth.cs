using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    [SerializeField] private int maxHealth = 3; // the starting health of the enemy.
    private int currentHealth; // the enemies current health.

    [SerializeField] private GameObject explosionPrefab; // a prefab for an explosion.
    [SerializeField] private float explosionFadeTime = 0.25f; // time until the explosion fades.

    [SerializeField] private GameObject[] debrisPrefabs; // an array of debris prefabs.

    private bool isDead = false; // flags if the enemy has died or not.

    private void Awake() {
        // Set current health with inspector value.
        currentHealth = maxHealth;
    }

    // When the enemy takes a hit reduce health by a set amount.
    public void Hit(int damage) {
        // Do nothing if the enemy is already dead.
        if (isDead) {
            return;
        }

        // Decuct the damage taken from current health.
        currentHealth -= damage;

        // Destroy the enemy if health goes below 0.
        if (currentHealth <= 0) {
            currentHealth = 0;
            Die();
        }
    }

    // Spawns an explosion effect, and debris when the enemy is killed.
    private void Die() {
        isDead = true; // flag as dead.

        // Spawn explosion and debris.
        SpawnExplosion();
        SpawnDebris();

        Destroy(gameObject); // destroy the enemy.
    }

    private void SpawnExplosion() {
        if (explosionPrefab == null) {
            // Return if there is no valid prefab.
            return;
        }

        // Get a random rotation to vary the visuals.
        float randomZ = Random.Range(0.0f, 360.0f);
        Quaternion randomRotation = Quaternion.Euler(0.0f, 0.0f, randomZ);

        // Create an explosion where the enemy died.
        GameObject explosion = Instantiate(explosionPrefab, transform.position, randomRotation);

        // Get a ref to the explosions fade script.
        Fade explosionFade = explosion.GetComponent<Fade>();
        if (explosionFade == null) {
            // If null, destroy after a set amount of time.
            Destroy(explosion, explosionFadeTime);
        } else {
            // Otherwise play the fade effect.
            explosionFade.Play(explosionFadeTime);
        }
    }

    private void SpawnDebris() {
        if (debrisPrefabs == null || debrisPrefabs.Length == 0) {
            // Return if there are no valid prefabs.
            return;
        }

        // Select a random piece of debris from the array of prefabs.
        int randomIndex = Random.Range(0, debrisPrefabs.Length);
        GameObject selectedDebris = debrisPrefabs[randomIndex];

        if (selectedDebris == null) {
            // Return if theres nothing selected.
            return; 
        }

        // Get a random rotation to vary the visuals.
        float randomZ = Random.Range(0.0f, 360.0f);
        Quaternion randomRotation = Quaternion.Euler(0.0f, 0.0f, randomZ);

        // Create the debris where the enemy died.
        Instantiate(selectedDebris, transform.position, randomRotation);
    }
}
