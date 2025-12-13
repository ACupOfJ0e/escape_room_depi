using UnityEngine;
using UnityEngine.Events;
public class slotHandler : MonoBehaviour
{
    [SerializeField] private int slotid;

    // We make this public so the Manager can check it later
    public bool isPlaced { get; private set; } = false;

    // Event just tells the manager "Something changed, please re-check the room"
    public event UnityAction onTabletPlaced;

    private void OnTriggerEnter(Collider other)
    {
        if (isPlaced) return; // Don't accept a second tablet if one is there

        if (other.CompareTag("Tablet"))
        {
            tabletHandler tablet = other.GetComponent<tabletHandler>();

            // Check ID
            if (tablet != null && tablet.GetID() == this.slotid)
            {
                isPlaced = true;

                // 1. Tell the tablet to freeze and animate
                tablet.InitiateSnap(this.transform);

                // 2. Notify the Manager
                onTabletPlaced?.Invoke();
            }
        }
    }
}
