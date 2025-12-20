using UnityEngine;

public class objectsound : MonoBehaviour
{
    // Array of impact sounds (drag multiple here)
    public AudioClip[] impactSounds;

    // Minimum force to make a sound
    public float minForce = 2f;

    private AudioSource audioSource;

    void Start()
    {
        // Create audio player on this object
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1f; // 3D sound
    }

    // Called when object hits something
    void OnCollisionEnter(Collision collision)
    {
        // Check collision force
        float force = collision.relativeVelocity.magnitude;

        if (force >= minForce && impactSounds.Length > 0)
        {
            // Pick random sound
            int randomIndex = Random.Range(0, impactSounds.Length);
            AudioClip randomSound = impactSounds[randomIndex];

            // Calculate volume based on force
            float volume = Mathf.Clamp01(force / 10f);

            // Play the random sound
            audioSource.PlayOneShot(randomSound, volume);
        }
    }
}