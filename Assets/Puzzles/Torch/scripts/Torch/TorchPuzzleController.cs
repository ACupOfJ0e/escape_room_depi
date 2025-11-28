using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

public class TorchPuzzleController : MonoBehaviour
{
    [Tooltip("All the torch sockets in the puzzle.")]
    public List<TorchSocket> sockets;

    [Tooltip("All the torches that are part of this puzzle.")]
    public List<Torch> torches;

    [Tooltip("The correct sequence of colors for the sockets to solve the puzzle.")]
    public List<TorchColor> solutionSequence;

    [Tooltip("Event to fire when the puzzle is successfully solved.")]
    public UnityEvent onPuzzleSolved;

    private bool isSolved = false;

    void Start()
    {
        // It's good practice to assign the controller to torches and sockets automatically.
        foreach (var torch in torches)
        {
            torch.puzzleController = this;
        }
        foreach (var socket in sockets)
        {
            socket.puzzleController = this;
        }

        RandomizeTorchesInitialPlacement();
    }

    /// <summary>
    /// Places torches in random sockets at the start.
    /// </summary>
    public void RandomizeTorchesInitialPlacement()
    {
        // Detach any existing torches from sockets
        foreach (var socket in sockets)
        {
            socket.DetachTorch();
        }

        // Create a shuffled list of torches
        List<Torch> shuffledTorches = torches.OrderBy(a => Random.value).ToList();

        // Place torches in sockets according to the shuffled list
        for (int i = 0; i < sockets.Count && i < shuffledTorches.Count; i++)
        {
            sockets[i].AttachTorch(shuffledTorches[i]);
        }
    }

    /// <summary>
    /// Checks if the current arrangement of torches matches the solution.
    /// </summary>
    public void CheckForSolution()
    {
        if (isSolved) return;

        for (int i = 0; i < sockets.Count; i++)
        {
            if (sockets[i].currentTorch == null || sockets[i].currentTorch.torchColor != sockets[i].correctColor)
            {
                return; // Not solved
            }
        }

        // If we get here, all sockets have the correct torch
        isSolved = true;
        Debug.Log("Puzzle Solved!");
        onPuzzleSolved.Invoke();
    }

    /// <summary>
    /// Assigns the correct colors from the solutionSequence to the sockets.
    /// This is intended to be called from the custom editor.
    /// </summary>
    public void AssignSolutionToSockets()
    {
        if (solutionSequence == null || sockets == null) return;

        for (int i = 0; i < sockets.Count; i++)
        {
            if (i < solutionSequence.Count)
            {
                sockets[i].correctColor = solutionSequence[i];
            }
        }
    }
}
