using UnityEngine;

public class room1_Manager : MonoBehaviour
{
    [SerializeField] private GameObject keystone;
    [SerializeField] private pyramidSlot[] puzzlesSlots;

    // Make sure to drag the object holding the 'keyplaceariser' script here in Inspector!
    public GameObject firstRoom;

    private void Start()
    {
        if (keystone) keystone.SetActive(false);

        foreach (var slot in puzzlesSlots)
        {
            // We use the event without arguments to match Room 3's logic
            slot.onCorrectPlacement += CheckAllSlotsFilled;
        }
    }

    // REMOVED "bool isfilledStatus" to match Room 3 and prevent delegate errors
    private void CheckAllSlotsFilled()
    {
        bool allFilled = true;

        // Loop through all slots to check status
        foreach (var slot in puzzlesSlots)
        {
            if (!slot.isFilled)
            {
                allFilled = false;
                break; // Optimization: Stop checking if one is empty
            }
        }

        // --- CRITICAL FIX: The logic must be INSIDE this If statement ---
        if (allFilled)
        {
            Debug.Log("Conditions met! Attempting to raise key..."); // Debug 1

            PuzzleSolved();

            if (firstRoom != null)
            {
                keyplaceariser key = firstRoom.GetComponent<keyplaceariser>();

                if (key != null)
                {
                    StartCoroutine(key.arise());
                    Debug.Log("Key Arise Coroutine Started."); // Debug 2
                }
                else
                {
                    Debug.LogError("CRITICAL: The 'firstRoom' object does not have a 'keyplaceariser' script attached!");
                }
            }
            else
            {
                Debug.LogError("CRITICAL: You forgot to assign 'firstRoom' in the Inspector!");
            }
        }
        else
        {
            // Optional: verify the puzzle is NOT solved yet
            // Debug.Log("Puzzle not yet solved.");
        }
    }

    private void PuzzleSolved()
    {
        if (keystone) keystone.SetActive(true);
        Debug.Log("Room 1 Puzzle Completed (Keystone Active)!");
    }

    private void OnDestroy()
    {
        foreach (var slot in puzzlesSlots)
        {
            slot.onCorrectPlacement -= CheckAllSlotsFilled;
        }
    }
}