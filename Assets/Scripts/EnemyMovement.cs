using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    // An enum for various movement types an enemy can use.
    public enum MovementType {
        StraightLeft,
        SlowThenFast,
        SineWave,
        EnterAndStop
    }

    // The enemies selected movement type.
    [SerializeField] private MovementType movementType = MovementType.StraightLeft;

    // The enemies default movespeed.
    [SerializeField] private float moveSpeed = 3.0f;

    // Amplitude and frequency for sine wave movement.
    [SerializeField] private float amp = 1.0f;
    [SerializeField] private float freq = 2.0f;

    // Slow duration and speed multiplier for slow then fast movement.
    [SerializeField] private float slowDuration = 1.2f;
    [SerializeField] private float speedMultipier = 2.0f;

    // Stop x position for boss and a flag to signal they have stopped.
    [SerializeField] float stopX = 5.0f;
    [SerializeField] bool hasStopped = false;

    private Vector3 startPos;
    private float startTime;

    private void Start() {
        // Store the starting position and time.
        startPos = transform.position;
        startTime = Time.time;
    }

    private void Update() {
        // Record time since spawn.
        float elapsedTime = Time.time - startTime;

        // Determine movement type and update.
        switch (movementType) {
            case MovementType.StraightLeft:
                StraightLeft();
                break;
            case MovementType.SlowThenFast:
                SlowThenFast(elapsedTime);
                break;
            case MovementType.SineWave:
                SineWave(elapsedTime);
                break;
            case MovementType.EnterAndStop:
                EnterAndStop(elapsedTime);
                break;
        }
    }

    // Move left across the screen in a straight line.
    private void StraightLeft() {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }

    // Move slow then speed up after some time.
    private void SlowThenFast(float elapsedTime) {
        float currentSpeed = moveSpeed;
        
        // Apply the speed multiplier its time.
        if (elapsedTime >= slowDuration) {
            currentSpeed = moveSpeed * speedMultipier;
        }

        transform.position += Vector3.left * currentSpeed * Time.deltaTime;
    }

    // Move in a sine wave pattern.
    private void SineWave(float elapsedTime) {
        Vector3 position = transform.position;

        // Move constantly along the x-axis.
        position.x -= moveSpeed * Time.deltaTime;

        // Oscillate on the y-axis.
        position.y = startPos.y + Mathf.Sin(elapsedTime * freq) * amp;

        transform.position = position;
    }

    // Enter the arena then stop moving.
    private void EnterAndStop(float elapsedTime) {
        if (hasStopped) {
            // If the boss has reached its target x-pos do nothing.
            return;
        }

        Vector3 position = transform.position;

        // Move left until reaching stopX.
        position.x = Mathf.MoveTowards(position.x, stopX, moveSpeed * Time.deltaTime);
        transform.position = position;

        // Stop when stopX is reached.
        if (transform.position.x <= stopX) {
            Vector3 stopPosition = transform.position;

            stopPosition.x = stopX;
            transform.position = stopPosition;

            hasStopped = true;
        }

    }
}
