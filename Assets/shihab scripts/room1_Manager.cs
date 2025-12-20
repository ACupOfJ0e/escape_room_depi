using UnityEngine;
using System.Collections;

public class room1_Manager : MonoBehaviour
{
    [SerializeField] private GameObject keystone;
    [SerializeField] private pyramidSlot[] puzzlesSlots;
    public GameObject firstRoom;
    private void Start()
    {
        keystone.SetActive(false);
        foreach (var slot in puzzlesSlots)
        {
            slot.onCorrectPlacement += CheckAllSlotsFilled;
        }
    }
    private void CheckAllSlotsFilled(bool isfilledStatus)
    {
        bool allFilled = true;
        foreach (var slot in puzzlesSlots)
        {
            if (!slot.isFilled)
                allFilled = false;
        }
        if (allFilled) {
            PuzzleSolved();
            keyplaceariser key = firstRoom.GetComponent<keyplaceariser>();
            StartCoroutine(key.arise());
        }
        Debug.Log("All slots are correctly filled! Puzzle solved.");
        // Additional logic for when the puzzle is solved can be added here
    }
    private void PuzzleSolved()
    {
        keystone.SetActive(true);
        // Logic to handle puzzle completion
        Debug.Log("Room 1 Puzzle Completed!");
        // You can add more actions here, like unlocking a door or triggering an event
    }
    private void OnDestroy()
    {
        foreach (var slot in puzzlesSlots)
        {
            slot.onCorrectPlacement -= CheckAllSlotsFilled;
        }
    }

}
