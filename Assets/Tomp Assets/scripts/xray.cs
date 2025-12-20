using UnityEngine;

public class xray : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Renderer>().material.renderQueue = 3002;
    }

    
}
