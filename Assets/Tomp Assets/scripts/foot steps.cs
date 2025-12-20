using UnityEngine;

public class footsteps : MonoBehaviour
{
    public AudioClip[] footstepSounds;
    public float distance = 1.2f;
    private AudioSource audioSource;
    private Vector3 lastPosition;
    private float distanceMoved;
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.7f;

        lastPosition = transform.position;

    }

    
    void Update()
    {
        Vector3 currentPos = transform.position;
        Vector3 movement = new Vector3(
            currentPos.x - lastPosition.x,
            0,
            currentPos.z - lastPosition.z
        );

        
        distanceMoved += movement.magnitude;

        
        if (distanceMoved >= distance)
        {
            PlayFootstep();
            distanceMoved = 0f; // Reset counter
        }

        // Remember position for next frame
        lastPosition = currentPos;
    }
    void PlayFootstep()
    {
        if (footstepSounds == null || footstepSounds.Length == 0)
        {
            Debug.LogWarning("No footstep sounds assigned!");
            return;
        }

        // Pick a RANDOM sound from the array
        int randomIndex = Random.Range(0, footstepSounds.Length);
        AudioClip randomSound = footstepSounds[randomIndex];

        // Play the random sound
        if (randomSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(randomSound);
        }
    }
}
