using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class keyplacment : MonoBehaviour
{
    public GameObject left;
    public GameObject right;
    public GameObject head;
    public GameObject main_door;
    public float durationz = 10f;
    public float durationy=2f;
    private float openingz = 8.07f;
    private float openingy = 7.15f;
    public GameObject door1;
    public GameObject door2;
    public GameObject door3;
    private int keyParts=0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        left.gameObject.SetActive(false);
        right.gameObject.SetActive(false);
        head.gameObject.SetActive(false);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("left")) 
        { 
        left.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(door3.GetComponent<doorcontroller>().CloseDoor());
            keyParts++;
        }
        else if (other.CompareTag("right"))
        {
            right.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(door1.GetComponent<doorcontroller>().CloseDoor());
            keyParts++;
        }
        else if (other.CompareTag("head"))
        {
            head.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(door2.GetComponent<doorcontroller>().CloseDoor());
            keyParts++;
        }
        if(keyParts == 3)
        {
            StartCoroutine(OpenDoor());
            AudioSource source = main_door.GetComponent<AudioSource>();
            source.Play();
        }
    }
    public IEnumerator OpenDoor()
    {
        // Use LOCAL position since we're a child object
        Vector3 startLocalPos = main_door.transform.localPosition;
        Vector3 endLocalPos = new Vector3(startLocalPos.x, startLocalPos.y, openingz);

        float elapsed = 0f;

        while (elapsed < durationz)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / durationz);

            // Interpolate LOCAL position
            float newZ = Mathf.Lerp(startLocalPos.z, endLocalPos.z, t);
            main_door.transform.localPosition = new Vector3(startLocalPos.x, startLocalPos.y, newZ);

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
            main_door.transform.localPosition = new Vector3(startLocalPos.x, newY, 8.071f);

            yield return null;
        }
        

    }
}
