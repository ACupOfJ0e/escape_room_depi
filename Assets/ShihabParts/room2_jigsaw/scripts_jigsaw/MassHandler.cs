using UnityEngine;
using UnityEngine.Events;
public class MassHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public event UnityAction<float> onMassChanged;
    private float currentMass = 0f;
  
    private void OnTriggerEnter(Collider other)
    {
        if(other == null || !other.CompareTag("weight_object")) return;
        Rigidbody rb = other.GetComponent<Rigidbody>();
            float massValue;
        Debug.Log("Trigger Entered");
        if (!other.CompareTag("Player") && rb != null)
            {
                massValue = rb.mass;
                AddMass(massValue);
            }   
    }
    private void OnTriggerExit(Collider other)
    {
        if(other == null || !other.CompareTag("weight_object")) return;
        Rigidbody rb = other.GetComponent<Rigidbody>();
        float massValue;
        Debug.Log("Trigger Exited");
        if (!other.CompareTag("Player") && rb != null)
        {
            massValue = rb.mass;
            SubtractMass(massValue);
        }
    }

    //Helper methods
    private void SubtractMass(float massToSubtract)
    {
        currentMass -= massToSubtract;
        Debug.Log("Current Mass: " + currentMass);
        onMassChanged?.Invoke(currentMass);
    }
    private void AddMass(float massToAdd)
    {
        currentMass += massToAdd;
        Debug.Log("Current Mass: " + currentMass);
        onMassChanged?.Invoke(currentMass);
    }
    public float GetCurrentMass()
    {
        return currentMass;
    }
}
