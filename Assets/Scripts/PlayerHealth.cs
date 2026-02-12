using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour {
    [SerializeField] private int maxHealth = 3; // the starting health of the player.
    private int currentHealth; // the players current health.

    [SerializeField] private int maxShieldCharges = 0;
    private int shieldCharges = 0; // the number shield charges the player has.

    public TMP_Text healthText; // to display health on the UI.
    public TMP_Text shieldText; // to display shield charges on the UI.

    public GameObject shieldSprite;

    public Animator animator; // A ref to the animator.

    private void Awake() {
        // Set current health with inspector value.
        currentHealth = maxHealth;

        // Get a ref to the animator component.
        animator = GetComponent<Animator>();

        RefreshHUD();
        RefreshShieldVisual();
    }

    // When the player takes a hit reduce health by a set amount.
    public void Hit(int damage) {
        // If the player has a shield take a charge but do no damage to hp.
        if (shieldCharges > 0) {
            shieldCharges -= 1;
            return;
        }

        currentHealth -= damage;

        if (currentHealth > 0 && animator != null) {
            animator.SetTrigger("Hit");
        }

        // Reload the scene if the players health goes below 0.
        if (currentHealth < 0) {
            RefreshHUD();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        RefreshHUD();
        RefreshShieldVisual();
    }

    // Heal the player by a set amount.
    public void Heal(int amount) {
        // Increase the players health.
        currentHealth += amount;

        // Ensure health does not go above the max.
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }

        RefreshHUD();
    }

    // Adds shield charges to the player.
    public void AddShield(int amount) {
        shieldCharges += amount;

        // Ensure shield charges does not go above the max.
        if (shieldCharges > maxShieldCharges) {
            shieldCharges = maxShieldCharges;
        }

        RefreshHUD();
        RefreshShieldVisual();
    }

    // Refresh the HUD with updated values.
    private void RefreshHUD() {
        if (healthText != null) {
            healthText.text = "HP: " + currentHealth + "/" + maxHealth;
        }

        if (shieldText != null) {
            shieldText.text = "Shield: " + shieldCharges + "/" + maxShieldCharges;
        }
    }

    // Refresh the shield visual
    private void RefreshShieldVisual() {
        // Check for a valid ref to the sprite.
        if (shieldSprite == null) {
            return;
        }

        // Toggle the shield sprite
        if (shieldCharges > 0) {
            shieldSprite.SetActive(true);
        } else { 
            shieldSprite.SetActive(false); 
        }
    }

    // Returns the players current health.
    public int GetHealth() {
        return currentHealth;
    }

    // Returns the players current shield charges.
    public int GetShieldCharges() {
        return shieldCharges;
    }
}
