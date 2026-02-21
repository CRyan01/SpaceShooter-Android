using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour {
    [SerializeField] private float padding = 2.0f; // padding so that objects are destoryed out of sight.


    // Toggles to enable/disable destroy behaviour based on the objects needs.
    [SerializeField] private bool destroyBelow = true;
    [SerializeField] private bool destroyAbove = true;
    [SerializeField] private bool destroyLeft = true;
    [SerializeField] private bool destroyRight = true;

    private Camera cam; // a ref to the main camera.

    void Start() {
        cam = Camera.main; // get a ref to the main camera.
    }

    void Update() {
        if (cam == null) {
            return;
        }

        // Get current bounds in world space.
        float leftX = cam.ViewportToWorldPoint(new Vector3(0f, 0.5f, 0.0f)).x - padding;
        float rightX = cam.ViewportToWorldPoint(new Vector3(1f, 0.5f, 0.0f)).x + padding;
        float bottomY = cam.ViewportToWorldPoint(new Vector3(0.5f, 0f, 0.0f)).y - padding;
        float topY = cam.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0.0f)).y + padding;

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
