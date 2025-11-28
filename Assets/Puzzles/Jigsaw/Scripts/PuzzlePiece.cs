using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
public class PuzzlePiece : MonoBehaviour
{
    [Header("Piece Settings")]
    [SerializeField] private Vector2Int correctCell;
    [SerializeField] private float maxGrabDistance = 3f;
    [SerializeField] private float snapDistance = 0.3f; // Distance to trigger snap
    
    [Header("Physics")]
    [SerializeField] private bool useGravityWhenNotGrabbed = false;
    
    // State
    public bool IsPlaced { get; private set; }
    public Vector2Int CurrentCell { get; private set; }
    
    // References
    private PuzzleGrid grid;
    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;
    
    // Initial transform
    private Vector3 startPosition;
    private Quaternion startRotation;
    
    // State tracking
    private bool isLocked = false;
    
    public void Initialize(PuzzleGrid puzzleGrid)
    {
        grid = puzzleGrid;
        
        // Cache components
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
        
        // Store initial transform
        startPosition = transform.position;
        startRotation = transform.rotation;
        
        // Configure rigidbody
        rb.useGravity = useGravityWhenNotGrabbed;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        
        // Subscribe to grab events
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
        
        // Subscribe to puzzle events
        grid.OnPuzzleCompleted += OnPuzzleCompleted;
        
        Debug.Log($"Initialized piece: {gameObject.name}, Correct cell: {correctCell}");
    }
    
    void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrabbed);
            grabInteractable.selectExited.RemoveListener(OnReleased);
        }
        
        if (grid != null)
        {
            grid.OnPuzzleCompleted -= OnPuzzleCompleted;
        }
    }
    
    void OnGrabbed(SelectEnterEventArgs args)
    {
        if (isLocked) return;
        
        // Remove from grid if placed
        if (IsPlaced)
        {
            grid.RemovePiece(this);
            IsPlaced = false;
        }
        
        // Enable physics while grabbed
        rb.useGravity = false;
        
        Debug.Log($"Grabbed piece: {gameObject.name}");
    }
    
    void OnReleased(SelectExitEventArgs args)
    {
        if (isLocked) return;
        
        Debug.Log($"Released piece: {gameObject.name} at {transform.position}");
        
        // Check if we're near the grid
        float distanceToGrid = Vector3.Distance(transform.position, grid.transform.position);
        
        if (distanceToGrid < snapDistance)
        {
            // Try to place on grid
            bool placed = grid.TryPlacePiece(this, transform.position);
            
            if (!placed)
            {
                // Placement failed, return to start
                ReturnToStart();
            }
        }
        else
        {
            // Too far from grid
            rb.useGravity = useGravityWhenNotGrabbed;
        }
    }
    
    /// <summary>
    /// Set the piece's placed state (called by grid)
    /// </summary>
    public void SetPlacedState(Vector2Int cell, Vector3 position, Vector3 forward)
    {
        IsPlaced = true;
        CurrentCell = cell;
        
        // Snap to position
        transform.position = position;
        transform.forward = forward;
        
        // Disable physics when placed
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
    }
    
    /// <summary>
    /// Check if piece is in correct position
    /// </summary>
    public bool IsInCorrectPosition()
    {
        return IsPlaced && CurrentCell == correctCell;
    }
    
    /// <summary>
    /// Return piece to starting position
    /// </summary>
    public void ReturnToStart()
    {
        IsPlaced = false;
        transform.position = startPosition;
        transform.rotation = startRotation;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = useGravityWhenNotGrabbed;
        
        Debug.Log($"Returned piece to start: {gameObject.name}");
    }
    
    void OnPuzzleCompleted()
    {
        // Lock piece in place
        isLocked = true;
        grabInteractable.enabled = false;
        rb.isKinematic = true;
        
        Debug.Log($"Locked piece: {gameObject.name}");
    }
    
    // Distance check for grabbing
    public void Update()
    {
        // This is now handled by XR Interaction Toolkit's distance settings
        // But we can add custom logic here if needed
    }
}