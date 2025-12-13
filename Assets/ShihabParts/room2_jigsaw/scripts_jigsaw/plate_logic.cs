using UnityEngine;
using UnityEngine.Events;
public class plate_logic : MonoBehaviour
{
    private int itemsOnPlate = 0;
    public event UnityAction onItemDropped;
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("weight_object")) return;
        itemsOnPlate++;
        onItemDropped?.Invoke();
    }
    private void OnTriggerExit(Collider other) {
        if (!other.CompareTag("weight_object")) return;
        itemsOnPlate--;
        onItemDropped?.Invoke();
    }

    public int GetItemsOnPlate()
    {
        return itemsOnPlate;
    }
}
