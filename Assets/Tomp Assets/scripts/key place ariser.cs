using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class keyplaceariser : MonoBehaviour
{
    public float durationy = 10f;
    private float arisingTarget = -0.08f;
    public GameObject key;
    private Rigidbody rb;
    private Collider[] col;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator arise()
    {
        // Use LOCAL position since we're a child object
        Vector3 startLocalPos = transform.localPosition;

        Vector3 endLocalPos = new Vector3(startLocalPos.y, arisingTarget, startLocalPos.z);
        AudioSource source = gameObject.GetComponent<AudioSource>();
        source.Play();
        float elapsed = 0f;
        rb=key.GetComponent<Rigidbody>();
        col= key.GetComponentsInChildren<Collider>();

        rb.isKinematic = true;
        rb.detectCollisions = false;
        foreach (Collider collider in col) { 
            collider.enabled = false;
        }

        while (elapsed < durationy)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / durationy);
            
            // Interpolate LOCAL position
            float newY = Mathf.Lerp(startLocalPos.y, endLocalPos.y, t);
            transform.localPosition = new Vector3(startLocalPos.x, newY, startLocalPos.z);

            yield return null;
        }
        rb.isKinematic = false;
        rb.detectCollisions = true;
        foreach (Collider collider in col)
        {
            collider.enabled = true;
        }

    }
    }
