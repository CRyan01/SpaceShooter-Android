using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    public float moveSpeed = 3.0f; // the enemies movespeed.

    private void Update() {
        // Move the enemy downwards.
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }
}
