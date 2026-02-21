using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    [SerializeField] private int maxHealth = 3; // the starting health of the enemy.
    private int currentHealth; // the enemies current health.

    [SerializeField] private GameObject explosionPrefab; // a prefab for an explosion.
    [SerializeField] private float explosionFadeTime = 0.25f; // time until the explosion fades.

    [SerializeField] private GameObject[] debrisPrefabs; // an array of debris prefabs.

    private bool isDead = false; // flags if the enemy has died or not.

    [SerializeField] private Animator animator; // a ref to the animator.

    [SerializeField] int collisionDamage = 1; // the amount of damage to the player on collision.
    [SerializeField] float collisionCooldown = 0.25f; // cooldown to prevent multiple hits.
    private float nextCollisionTime = 0.0f; // time until another collision is allowed.

    [SerializeField] private int scoreValue = 10; // how much score the player recives for the kill.

    [SerializeField] private bool isFinalBoss = false; // flag if this enemy is the final boss.

    private void Awake() {
        // Set current health with inspector value.
        currentHealth = maxHealth;

        // Get a ref to the animator component.
        animator = GetComponent<Animator>();
    }

    // When the enemy takes a hit reduce health by a set amount.
    public void Hit(int damage) {
        // Do nothing if the enemy is already dead.
        if (isDead) {
            return;
        }

        // Decuct the damage taken from current health.
        currentHealth -= damage;

        if (currentHealth > 0 && animator != null) {
            animator.SetTrigger("Hit");
        }

        // Destroy the enemy if health goes below 0.
        if (currentHealth <= 0) {
            currentHealth = 0;
            Die();
        }
    }

    // Spawns an explosion effect, and debris when the enemy is killed.
    private void Die() {
        isDead = true; // flag as dead.

        // Play a sound effect.
        AudioManager.Instance.Explosion();

        // Spawn explosion and debris.
        SpawnExplosion();
        SpawnDebris();

        // Add score to the player.
        if (ScoreManager.Instance != null) {
            ScoreManager.Instance.AddScore(scoreValue);
        }

        // Display the victory screen and end the game.
        if (isFinalBoss) {
            GameOverScreen.Instance.EndGame(true);
        }

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

    // When the enemy collides with another object.
    private void OnTriggerEnter2D(Collider2D other) {
        if (isDead) {
            // Do nothing if the enemy is dead.
            return;
        }

        if (Time.time < nextCollisionTime) {
            // If there was recently a collision do nothing.
            return;
        }

        // Otherwise damage the player and destroy this enemy.
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null) {
            playerHealth.Hit(collisionDamage);
            nextCollisionTime = Time.time + collisionCooldown;
            Die();
        }
    }
}
