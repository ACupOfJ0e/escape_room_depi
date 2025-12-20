using UnityEngine;
using UnityEngine.Events;
public class pyramidSlot : MonoBehaviour
{
    [SerializeField] private int slotId;
    public bool isFilled = false;
    public event UnityAction<bool> onCorrectPlacement;
    public void OnTriggerEnter(Collider other)
    {
        if(isFilled) return;

        if (other.tag == "pyramidToken")
        {
            pyramid1 token = other.GetComponent<pyramid1>();
            if (token != null && token.getTokenId() == slotId)
            {
                isFilled = true;
                onCorrectPlacement?.Invoke(isFilled);
                token.OnPlacedCorrectly();
                Debug.Log("Token " + token.getTokenId() + " correctly placed in slot " + slotId);
                // Additional logic for correct placement can be added here
            }
            
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "pyramidToken")
        {
            pyramid1 token = other.GetComponent<pyramid1>();
            if (token != null && token.getTokenId() != slotId)
            {
                isFilled = false;
                onCorrectPlacement?.Invoke(isFilled);
                
                Debug.Log("Token " + token.getTokenId() + " removed from slot " + slotId);
                // Additional logic for removal can be added here
            }
        }
    }
}

