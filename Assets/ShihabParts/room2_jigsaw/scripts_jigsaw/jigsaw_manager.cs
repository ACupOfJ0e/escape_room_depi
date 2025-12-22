using UnityEngine;

public class jigsaw_manager : MonoBehaviour
{
    [SerializeField] private MassHandler massHandler_left;
    [SerializeField] private MassHandler massHandler_right;
    public GameObject thirdRoom;

    [SerializeField] private GameObject keyStone;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool solved = false;
    void Start()
    {
        keyStone.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void CheckMasses(float _)
    {
        float leftMass = massHandler_left.GetCurrentMass();
        float rightMass = massHandler_right.GetCurrentMass();
        if (Mathf.Abs(leftMass - rightMass) < 0.1f && leftMass > 0)
        {
            if(solved) return;
            keyStone.SetActive(true);
            Debug.Log("Both sides have enough mass. Jigsaw puzzle solved!");
            keyplaceariser key = thirdRoom.GetComponent<keyplaceariser>();
            StartCoroutine(key.arise());
            solved = true;
            // Add further logic here for when the puzzle is solved
        }
        else
        {
            Debug.Log("Waiting for both sides to have enough mass...");
        }
    }

    private void OnEnable()
    {
        massHandler_left.onMassChanged += CheckMasses;
        massHandler_right.onMassChanged += CheckMasses;
    }
    private void OnDisable()
    {
        massHandler_left.onMassChanged -= CheckMasses;
        massHandler_right.onMassChanged -= CheckMasses;
    }
}
