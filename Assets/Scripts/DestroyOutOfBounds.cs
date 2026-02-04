using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour {
    [SerializeField] private float padding = 2.0f; // padding so that objects are destoryed out of sight.


    // Toggles to enable/disable destroy behaviour based on the objects needs.
    [SerializeField] private bool destroyBelow = true;
    [SerializeField] private bool destroyAbove = true;
    [SerializeField] private bool destroyLeft = true;
    [SerializeField] private bool destroyRight = true;

    private Camera camera; // a ref to the main camera.

    void Start() {
        camera = Camera.main; // get a ref to the main camera.
    }

    void Update() {
        if (camera == null) {
            return;
        }

        // Get current bounds in world space.
        float leftX = camera.ViewportToWorldPoint(new Vector3(0f, 0.5f, 0.0f)).x - padding;
        float rightX = camera.ViewportToWorldPoint(new Vector3(1f, 0.5f, 0.0f)).x + padding;
        float bottomY = camera.ViewportToWorldPoint(new Vector3(0.5f, 0f, 0.0f)).y - padding;
        float topY = camera.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0.0f)).y + padding;

        // Get the objects current position.
        Vector3 currentPosition = transform.position;

        // Check each side and destroy if necessary.
        if (destroyBelow && currentPosition.y < bottomY) {
            Destroy(gameObject);
        } else if (destroyAbove && currentPosition.y > topY) {
            Destroy(gameObject);
        } else if (destroyLeft && currentPosition.x < leftX) {
            Destroy(gameObject);
        } else if (destroyRight && currentPosition.x > rightX) {
            Destroy(gameObject);
        }
    }
}
