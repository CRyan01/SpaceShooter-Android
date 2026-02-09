using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float moveSpeed = 18.0f; // the players move speed.
    public float padding = 0.6f; // distance between the player and screen edge.

    public Transform firePoint; // where player fired bullets spawn.
    public GameObject bulletPrefab; // the bullet object prefab.
    public float fireRate = 8.0f; // how many bullets are fired a second.

    Camera camera; // reference to the main camera.

    float xMin, xMax; // left and right movement limits.
    float targetX; // target x position based on input.
    float nextShotTime; // time when the next bullet can be fired.

    private Coroutine fireRateRoutine; // a coroutine to increase the players firerate.
    private float baseFireRate; // the players base fire rate.

    void Start() {
        camera = Camera.main; // get a ref to the main camera.
        GetBounds(); // get the screen bounds.
        targetX = transform.position.x; // set the target position to the current position.

        baseFireRate = fireRate; // store the base fire rate to change it back later.
    }

    void Update() {
        ReadInput(); // read player input.
        Move(); // move the player towards the target position.
        AutoFire(); // fire bullets automatically based on fireRate.
    }

    // Calculate the left and right screen bounds in world space.
    void GetBounds() {
        // Convert the left and right sides of the viewport to world space.
        float left = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        float right = camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        // Apply padding to prevent clipping.
        xMin = left + padding;
        xMax = right - padding;
    }

    // Read the players input depending on platform.
    void ReadInput() {
#if UNITY_EDITOR || UNITY_STANDALONE
        // Get mouse input for editor testing.
        if (Input.GetMouseButton(0)) {
            var worldPosition = camera.ScreenToWorldPoint(Input.mousePosition);
            targetX = worldPosition.x;
        }
#else
        // Get touch input for android.
        if (Input.touchCount > 0) {
            var touchInput = Input.GetTouch(0);
            var worldPosition = camera.ScreenToWorldPoint(touchInput.position);
            targetX = worldPosition.x;
        }
#endif
    }

    // Move the ship horizontally.
    void Move() {
        // Clamp the target position within screen bounds.
        float x = Mathf.Clamp(targetX, xMin, xMax);

        // Move towards the target x position.
        var position = transform.position;
        position.x = Mathf.MoveTowards(position.x, x, moveSpeed * Time.deltaTime);
        transform.position = position;
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
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            // Set the time for the next shot.
            nextShotTime = Time.time + (1.0f / fireRate);
        }
    }

    // Adds a fire rate boost to the player for a set duration.
    public void AddFireRate(float amount, float duration) {
        // Check if a previous fire rate routine is active.
        if (fireRateRoutine != null) {
            // If so stop it and reset fire rate.
            StopCoroutine(fireRateRoutine);
            fireRate = baseFireRate;
        }

        // Start a new fire rate boost routine.
        fireRateRoutine = StartCoroutine(FireRateBoostRoutine(amount, duration));
    }

    private IEnumerator FireRateBoostRoutine(float amount, float duration) {
        // Apply the boosted fire rate value with a clamped limit.
        fireRate = Mathf.Clamp(baseFireRate + amount, 1.0f, 20.0f);

        // Run for a set amount of time.
        yield return new WaitForSeconds(duration);

        // When time runs out restore the normal fire rate, and routine state.
        fireRate = baseFireRate;
        fireRateRoutine = null;
    }
}
