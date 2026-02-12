using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour {
    // A ref to the spriterenderer component of the object.
    [SerializeField] SpriteRenderer spriteRenderer;

    private void Awake() {
        // Get a ref to the sprite renderer component.
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Play(float duration) {
        StartCoroutine(FadeRoutine(duration));
    }

    private IEnumerator FadeRoutine(float fadeDuration) {
        if (spriteRenderer == null) {
            // If there is no valid ref to the sprite render component,
            // just destroy the object after a set amount of time, and end the routine.
            Destroy(gameObject, fadeDuration);
            yield break;
        }

        // Get the starting color of the object from the renderer.
        Color startingColor = spriteRenderer.color;

        // Track elapsed time.
        float elapsedTime = 0.0f;

        while (elapsedTime > fadeDuration) {
            // Update elapsed time.
            elapsedTime += Time.deltaTime;

            // Calculate how much of the duration has passed.
            float time = Mathf.Clamp01(elapsedTime / fadeDuration);

            Color newColor = startingColor;

            // Interpolate to reduce the alpha of the color based on elapsed time.
            newColor.a = Mathf.Lerp(1.0f, 0.0f, time);

            yield return null;
        }

        Destroy(gameObject); // destroy when finished.
    }
}
