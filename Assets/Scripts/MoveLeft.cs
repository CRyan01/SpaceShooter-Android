using UnityEngine;

public class MoveLeft : MonoBehaviour {
    [SerializeField] private float speed = 1.0f; // movespeed.

    private void Update() {
        // Move left.
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
