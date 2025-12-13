using UnityEngine;
using System.Collections;
public class tabletHandler : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private GameObject grabbableComponent; // The script that lets you grab it

    // How deep inside the wall it goes (adjust this!)
    [SerializeField] private Vector3 insideWallOffset = new Vector3(0, 0, 0.02f);
    [SerializeField] private float snapSpeed = 0.2f;

    private Rigidbody rb;
    private bool isLocked = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public int GetID()
    {
        return id;
    }

    // The Slot calls this function when the tablet enters the zone
    public void InitiateSnap(Transform wallSlotTransform)
    {
        if (isLocked) return; // Don't snap twice
        isLocked = true;

        // 1. Kill Physics
        if (grabbableComponent != null) grabbableComponent.SetActive(false); // Stop player holding it
        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // 2. Start the Slide Animation
        StartCoroutine(AnimateSnap(wallSlotTransform));
    }

    private IEnumerator AnimateSnap(Transform target)
    {
        // STEP 1: Align Rotation immediately to look flat on the wall
        // We rotate smoothly to match the slot's rotation
        Quaternion startRot = transform.rotation;
        Quaternion endRot = target.rotation;

        // STEP 2: Move to the Surface (Position 0)
        Vector3 startPos = transform.position;
        Vector3 surfacePos = target.position;

        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime * snapSpeed;
            // Smoothly move to surface
            transform.position = Vector3.Lerp(startPos, surfacePos, t);
            transform.rotation = Quaternion.Lerp(startRot, endRot, t);
            yield return null;
        }

        // STEP 3: Slide "Inside" the wall (The offset)
        // We use 'TransformDirection' so it moves "Inwards" relative to the wall's rotation
        Vector3 deepPos = surfacePos - (target.forward * insideWallOffset.z);

        t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime * snapSpeed;
            transform.position = Vector3.Lerp(surfacePos, deepPos, t);
            yield return null;
        }
    }
}
