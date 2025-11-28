// Assets/final_proj_test/scripts/Editor/TorchPuzzleControllerEditor.cs
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TorchPuzzleController))]
public class TorchPuzzleControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        DrawDefaultInspector();

        TorchPuzzleController controller = (TorchPuzzleController)target;

        // Add a space for better layout
        EditorGUILayout.Space();

        // Button to assign the solution sequence to the sockets
        if (GUILayout.Button("Assign Solution to Sockets"))
        {
            if (controller.sockets.Count != controller.solutionSequence.Count)
            {
                EditorUtility.DisplayDialog("Solution Mismatch",
                    "The number of sockets and the number of colors in the solution sequence must be the same.", "OK");
            }
            else
            {
                Undo.RecordObjects(controller.sockets.ToArray(), "Assign Solution to Sockets");
                controller.AssignSolutionToSockets();
                EditorUtility.DisplayDialog("Success", "Correct colors have been assigned to the sockets.", "OK");
            }
        }

        // Button to randomize torch placement for testing
        if (GUILayout.Button("Randomize Torch Placement"))
        {
            if (Application.isPlaying)
            {
                controller.RandomizeTorchesInitialPlacement();
            }
            else
            {
                EditorUtility.DisplayDialog("Editor Mode", "This function can only be used in Play Mode.", "OK");
            }
        }
    }
}
