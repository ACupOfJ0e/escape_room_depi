// Assets/final_proj_test/scripts/TorchSocket.cs
using UnityEngine;

public class TorchSocket : MonoBehaviour
{
    public TorchColor correctColor;
    public Torch currentTorch { get; private set; }
    public TorchPuzzleController puzzleController;

    [Tooltip("The distance within which a dropped torch will snap to this socket.")]
    public float snapDistance = 0.5f;

    /// <summary>
    /// Attaches a torch to this socket, snapping it into place.
    /// </summary>
    public void AttachTorch(Torch torch)
    {
        if (currentTorch != null)
        {
            // Optional: Handle case where socket is already occupied
            Debug.LogWarning($"Socket {gameObject.name} is already occupied.");
            return;
        }

        currentTorch = torch;
        torch.currentSocket = this;

        // Snap torch to socket position and rotation
        torch.transform.SetParent(transform);
        torch.transform.localPosition = Vector3.zero;
        torch.transform.localRotation = Quaternion.identity;

        Rigidbody torchRb = torch.GetComponent<Rigidbody>();
        if (torchRb != null)
        {
            torchRb.isKinematic = true;
        }

        puzzleController?.CheckForSolution();
    }

    /// <summary>
    /// Detaches the current torch from this socket.
    /// </summary>
    public void DetachTorch()
    {
        if (currentTorch != null)
        {
            currentTorch.transform.SetParent(null); // Unparent
            currentTorch.currentSocket = null;
            currentTorch = null;
        }
    }
}
