using UnityEngine;

public class pyramid1 : MonoBehaviour
{
    [SerializeField] private int tokenId;
    [SerializeField] private pyramidSlot pyramidSlot;

    private Vector3 slotLocation;
    [SerializeField] private Vector3 offset = new Vector3(0f,1f,0f);
    [SerializeField]private GameObject grabbableComponent;

    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        slotLocation = pyramidSlot.transform.position;
        
    }
    public int getTokenId() { return tokenId; }

    public void OnPlacedCorrectly()
    {
        this.transform.position = slotLocation+ offset;
        this.transform.rotation = default;
        grabbableComponent.SetActive(false);
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
