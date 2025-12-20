using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using static UnityEngine.GraphicsBuffer;

public class doorcontroller : MonoBehaviour
{
    private float openingx = -11.093f;
    private float openingy = 4.652f;
    private float closingx = -10.93556f;
    private float closingy = 1.574847f;
    public float durationx = 2f;
    public float durationy = 10f;
    public float length = 0f;
    public AudioClip doorSoundClose;
    public AudioClip doorSoundOpen;
    private bool start=true;


    private AudioSource source;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.volume = 0.5f;
        source.spatialBlend = 1f;
        StartCoroutine(OpenDoor());
        


    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator OpenDoor()
    {
        // Use LOCAL position since we're a child object
        Vector3 startLocalPos = transform.localPosition;
        Vector3 endLocalPos = new Vector3(openingx, startLocalPos.y, startLocalPos.z);
        
        float elapsed = 0f;
        if (start)
        {
            yield return new WaitForSeconds(length);
            start=false;
        }
        source.PlayOneShot(doorSoundOpen);
        while (elapsed < durationx)
        {
            
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / durationx);

            // Interpolate LOCAL position
            float newX = Mathf.Lerp(startLocalPos.x, endLocalPos.x, t);
            transform.localPosition = new Vector3(newX, startLocalPos.y, startLocalPos.z);

            yield return null;
        }
        elapsed = 0f;

        endLocalPos = new Vector3(startLocalPos.y, openingy, startLocalPos.z);
        while (elapsed < durationy)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / durationy);

            // Interpolate LOCAL position
            float newY = Mathf.Lerp(startLocalPos.y, endLocalPos.y, t);
            transform.localPosition = new Vector3(-11.093f, newY, startLocalPos.z);

            yield return null;
        }
        

    }
    public IEnumerator CloseDoor()
    {
        source.PlayOneShot(doorSoundClose);
        // Use LOCAL position since we're a child object
        Vector3 startLocalPos = transform.localPosition;

        Vector3 endLocalPos = new Vector3(startLocalPos.y, closingy, startLocalPos.z);

        float elapsed = 0f;

        
        while (elapsed < durationy)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / durationy);

            // Interpolate LOCAL position
            float newY = Mathf.Lerp(startLocalPos.y, endLocalPos.y, t);
            transform.localPosition = new Vector3(-11.093f, newY, startLocalPos.z);

            yield return null;
        }
         elapsed = 0f;
         endLocalPos = new Vector3(closingx, startLocalPos.y, startLocalPos.z);
        while (elapsed < durationx)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / durationx);

            // Interpolate LOCAL position
            float newX = Mathf.Lerp(startLocalPos.x, endLocalPos.x, t);
            transform.localPosition = new Vector3(newX, 1.574847f, startLocalPos.z);

            yield return null;
        }
    }
}

