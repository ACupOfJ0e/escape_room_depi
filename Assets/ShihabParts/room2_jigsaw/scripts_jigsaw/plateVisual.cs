using UnityEngine;

public class plateVisual : MonoBehaviour
{
    [SerializeField] private MassHandler massHandler_plate;
    [SerializeField] private plate_logic plateLogic;
    private Vector3 initialPosition;
    private bool canMove = false;
    private Vector3 targetPosition;
    private float moveSpeed = 0.02f;
    private Vector3 offsetVector = new Vector3(0f, -0.05f, 0f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition;
    }

    private void Update()
    {
        if (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
        }
    }

    private void RecalculateTargetPosition()
    {
        if (plateLogic.GetItemsOnPlate() <= 0)
        {
            targetPosition = initialPosition;
            return;
        }
        else
        {
            float plateMass = massHandler_plate.GetCurrentMass();
            if (plateMass >= 5f)
            {
                targetPosition = initialPosition + offsetVector + offsetVector;
                Debug.Log("Plate has enough mass.");
                // Add further logic here for when the plate has enough mass
            }
            else if (plateMass >= 2.5f && plateMass < 5f)
            {
                targetPosition = initialPosition + offsetVector;
                Debug.Log("Plate has moderate mass.");
            }
            else
            {
                targetPosition = initialPosition;
                Debug.Log("Plate does not have enough mass yet...");
            }
        }

    }
    private void movePlateAccordingToMass(float _)
    {
        if (!canMove) return;
        float plateMass = massHandler_plate.GetCurrentMass();

        if (plateMass >= 5f)
        {
            targetPosition = initialPosition + offsetVector + offsetVector;
            Debug.Log("Plate has enough mass.");
            // Add further logic here for when the plate has enough mass
        }
        else if (plateMass >= 2.5f && plateMass < 5f)
        {
            targetPosition = initialPosition + offsetVector;
            Debug.Log("Plate has moderate mass.");
        }
        else
        {
            targetPosition = initialPosition;
            Debug.Log("Plate does not have enough mass yet...");
        }
    }

    private void checkSurface()
    {
        if (plateLogic.GetItemsOnPlate() > 0)
        {
            //transform.position = initialPosition;
            canMove = true;
            Debug.Log("Plate is empty, resetting position.");
        }
        else
        {
            canMove = false;
        }
    }
    private void OnEnable()
    {
        //massHandler_plate.onMassChanged += movePlateAccordingToMass;
        //plateLogic.onItemDropped += checkSurface;
        massHandler_plate.onMassChanged += (vol) => RecalculateTargetPosition();
        plateLogic.onItemDropped += RecalculateTargetPosition;
    }
    private void OnDisable()
    {
        massHandler_plate.onMassChanged -= (vol) => RecalculateTargetPosition();
        plateLogic.onItemDropped -= RecalculateTargetPosition;
        //massHandler_plate.onMassChanged -= movePlateAccordingToMass;
        //plateLogic.onItemDropped -= checkSurface;
    }
}