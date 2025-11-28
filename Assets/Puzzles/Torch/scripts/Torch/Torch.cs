// Assets/final_proj_test/scripts/Torch.cs
using UnityEngine;

public class Torch : MonoBehaviour
{
    public TorchColor torchColor;
    public TorchSocket currentSocket;
    public TorchPuzzleController puzzleController;

    [Tooltip("The renderer for the part of the torch that should change color (e.g., the flame).")]
    [SerializeField] private Renderer torchRenderer;

    private bool isHeld = false;
    private MaterialPropertyBlock propBlock;
    private Rigidbody rb;
    private Collider torchCollider;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        torchCollider = GetComponent<Collider>();
        propBlock = new MaterialPropertyBlock(); // Initialize the property block once.
        UpdateTorchVisualColor();
    }

    /// <summary>
    /// This is called in the editor when the script is loaded or a value is changed in the Inspector.
    /// </summary>
    private void OnValidate()
    {
        // We need to check for nulls because this can be called before Awake() in the editor.
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (torchCollider == null) torchCollider = GetComponent<Collider>();
        if (propBlock == null) propBlock = new MaterialPropertyBlock();
        UpdateTorchVisualColor();
    }

    public void OnPickup()
    {
        isHeld = true;
        if (currentSocket != null)
        {
            currentSocket.DetachTorch();
            currentSocket = null;
        }
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    public void OnDrop()
    {
        isHeld = false;
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        // Find the closest available socket to snap to
        TorchSocket closestSocket = FindClosestAvailableSocket();
        if (closestSocket != null)
        {
            closestSocket.AttachTorch(this);
        }
    }

    private TorchSocket FindClosestAvailableSocket()
    {
        if (puzzleController == null) return null;

        TorchSocket closest = null;
        float minDistance = float.MaxValue;

        foreach (var socket in puzzleController.sockets)
        {
            if (socket.currentTorch == null) // Socket is available
            {
                float distance = Vector3.Distance(transform.position, socket.transform.position);
                if (distance < minDistance && distance <= socket.snapDistance)
                {
                    minDistance = distance;
                    closest = socket;
                }
            }
        }
        return closest;
    }

    /// <summary>
    /// Updates the material color of the torch based on the selected torchColor enum.
    /// </summary>
    private void UpdateTorchVisualColor()
    {
        if (torchRenderer == null)
        {
            // Try to find a renderer if one isn't assigned.
            torchRenderer = GetComponentInChildren<Renderer>();
            if (torchRenderer == null)
            {
                Debug.LogWarning($"Torch '{gameObject.name}' is missing a Renderer. Cannot set color.", this);
                return;
            }
        }

        // For runtime, MaterialPropertyBlock is the most performant way to change color.
        if (Application.isPlaying)
        {
            // Get the current value of the property block to avoid overwriting other properties.
            torchRenderer.GetPropertyBlock(propBlock);
            propBlock.SetColor("_Color", GetColorFromEnum(torchColor));
            torchRenderer.SetPropertyBlock(propBlock);
        }
        else // For editor updates, we need to modify the material directly.
        {
            // Accessing .material creates a new material instance if one doesn't exist for this renderer.
            // This prevents changing the shared material asset and affecting all prefabs.
            // Unity handles the cleanup of these instances automatically in the editor.
            torchRenderer.material.color = GetColorFromEnum(torchColor);
        }
    }

    private Color GetColorFromEnum(TorchColor colorEnum)
    {
        switch (colorEnum)
        {
            case TorchColor.Red: return Color.red;
            case TorchColor.Green: return Color.green;
            case TorchColor.Blue: return Color.blue;
            case TorchColor.Yellow: return Color.yellow;
            case TorchColor.Purple: return new Color(0.5f, 0, 0.5f); // Magenta is often too bright
            case TorchColor.White: return Color.white;
            default: return Color.gray; // For None or unhandled
        }
    }
}
