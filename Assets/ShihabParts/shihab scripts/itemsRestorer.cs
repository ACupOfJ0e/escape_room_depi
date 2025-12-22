using UnityEngine;

public class itemsRestorer : MonoBehaviour
{
    [SerializeField] private Transform originalPosition;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null)
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            collision.transform.position = new Vector3(collision.transform.position.x, originalPosition.position.y, collision.transform.position.z);
        }
    }
}
