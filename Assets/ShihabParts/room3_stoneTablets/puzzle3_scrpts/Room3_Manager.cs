using UnityEngine;

public class Room3_Manager : MonoBehaviour
{
    [SerializeField] private slotHandler[] slots;
    [SerializeField] private GameObject keyStone;
    public GameObject secondRoom;

    void Start()
    {
        keyStone.SetActive(false);

        // Subscribe to every slot
        foreach (var slot in slots)
        {
            // When any slot changes, run the 'CheckAllSlots' function
            slot.onTabletPlaced += CheckAllSlotsFilled;
        }
    }

    private void CheckAllSlotsFilled()
    {
        bool allFilled = true;

        // Check EVERY slot. If even one is empty, we fail.
        foreach (var slot in slots)
        {
            if (slot.isPlaced == false)
            {
                allFilled = false;
                break; // No need to check the rest
            }
        }

        if (allFilled)
        {
            PuzzleSolved();
            keyplaceariser key = secondRoom.GetComponent<keyplaceariser>();
            StartCoroutine(key.arise());
        }
    }

    private void PuzzleSolved()
    {
        Debug.Log("Room 3 Puzzle Completed!");
        keyStone.SetActive(true);
    }
}
