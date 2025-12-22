using UnityEngine;
using UnityEngine.Events;

public class pyramidSlot : MonoBehaviour
{
    [SerializeField] private int slotId;
    public bool isFilled = false;
    public event UnityAction onCorrectPlacement;

    // This flag ensures the puzzle piece never pops out accidentally
    private bool isLocked = false;

    public void OnTriggerEnter(Collider other)
    {
        // 1. If it's already solved, ignore everything.
        if (isLocked) return;

        // 2. Check Tag (using CompareTag is slightly faster/safer than ==)
        if (other.CompareTag("pyramidToken"))
        {
            pyramid1 token = other.GetComponent<pyramid1>();

            // 3. Check ID: Does the Token ID match this Slot ID?
            if (token != null && token.getTokenId() == slotId)
            {
                // --- SUCCESS LOGIC ---
                isFilled = true;
                isLocked = true; // Lock it immediately so it can't be bumped out

                // Snap the token (assuming this function handles position/rotation)
                token.OnPlacedCorrectly();

                // Notify the Manager
                onCorrectPlacement?.Invoke();

                Debug.Log($"SUCCESS: Token {token.getTokenId()} LOCKED into Slot {slotId}");
            }
            else
            {
                Debug.Log("Wrong token placed. Ignoring.");
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        // Since we lock the slot immediately on success, 
        // we DO NOT want OnTriggerExit to ever run for the correct token.
        // It should only run if we haven't solved it yet (e.g. removing a wrong token).
        if (isLocked) return;

        // NOTE: Your previous code had logic here to set isFilled = false.
        // But since 'isFilled' only becomes true when we Lock it, 
        // we don't actually need to do anything here. 
        // The slot remains 'false' (empty) until the correct item locks it.
    }
}