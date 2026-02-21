using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;

    // Sources
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;

    // Clips
    [SerializeField] private AudioClip playerFireClip;
    [SerializeField] private AudioClip enemyFireClip;
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private AudioClip pickupClip;

    private void Awake() {
        // Enforce a single instance.
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Plays a specified audio clip.
    private void PlaySFX(AudioClip clip) {
        if (clip == null || sfxSource == null) {
            return;
        }
        sfxSource.PlayOneShot(clip);
    }

    // When the player fires.
    public void PlayerFired() {
        PlaySFX(playerFireClip);
    }

    // When an enemy fires.
    public void EnemyFired() {
        PlaySFX(enemyFireClip);
    }

    // When an explosion occurs.
    public void Explosion() {
        PlaySFX(explosionClip);
    }

    // When a pickup is activated
    public void Pickup() {
        PlaySFX(pickupClip);
    }
}
