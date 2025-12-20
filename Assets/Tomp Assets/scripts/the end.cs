using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;
public class theend : MonoBehaviour
{
    
    public float durationy = 6.22f;
    private float closingy=10f;
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(CloseDoor());
        }
    }
    public IEnumerator CloseDoor()
{
    
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
        transform.localPosition = new Vector3(0.23f, newY, startLocalPos.z);

        yield return null;
    } }
}
