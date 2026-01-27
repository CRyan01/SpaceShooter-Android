using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    public float speed = 18.0f; // the bullets speed.
    public float lifetime = 3.0f; // time in seconds before the bullet is destroyed.

    void Start() {
        Destroy(gameObject, lifetime);
    }

    void Update() {
        // Move the bullet upwards at a given speed.
        transform.position += Vector3.up * speed * Time.deltaTime;
    }
}
