// ============================================================================
// PuzzleGrid.cs - The main puzzle board manager
// ============================================================================
using System;
using UnityEngine;

public class PuzzleGrid : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private int columns = 3;
    [SerializeField] private int rows = 3;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private float snapHeight = 0.01f; // Height above grid surface
    
    [Header("References")]
    [SerializeField] private PuzzlePiece[] pieces;
    
    [Header("Debug")]
    [SerializeField] private bool showDebugGizmos = true;
    
    // Events
    public event Action OnPuzzleCompleted;
    public event Action OnPuzzleFailed;
    
    // Grid data
    private PuzzlePiece[,] gridState;
    private Vector3 gridOrigin;
    
    void Awake()
    {
        InitializeGrid();
    }
    
    void Start()
    {
        // Auto-find pieces if not assigned
        if (pieces == null || pieces.Length == 0)
        {
            pieces = FindObjectsByType<PuzzlePiece>(FindObjectsSortMode.None);
            Debug.Log($"Auto-found {pieces.Length} puzzle pieces");
        }
        
        // Register all pieces with this grid
        foreach (var piece in pieces)
        {
            piece.Initialize(this);
        }
    }
    
    void InitializeGrid()
    {
        gridState = new PuzzlePiece[columns, rows];
        
        // Calculate grid origin (bottom-left corner in local space)
        float gridWidth = columns * cellSize;
        float gridHeight = rows * cellSize;
        gridOrigin = transform.position - transform.right * (gridWidth / 2f) - transform.up * (gridHeight / 2f);
    }
    
    /// <summary>
    /// Get the grid cell (col, row) from a world position
    /// </summary>
    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        // Transform to local space relative to grid origin
        Vector3 localPos = worldPos - gridOrigin;
        
        // Project onto grid plane
        float xDist = Vector3.Dot(localPos, transform.right);
        float yDist = Vector3.Dot(localPos, transform.up);
        
        // Convert to cell coordinates
        int col = Mathf.FloorToInt(xDist / cellSize);
        int row = Mathf.FloorToInt(yDist / cellSize);
        
        return new Vector2Int(col, row);
    }
    
    /// <summary>
    /// Get world position of a grid cell center
    /// </summary>
    public Vector3 GridToWorld(Vector2Int cell)
    {
        Vector3 cellCenter = gridOrigin 
            + transform.right * (cell.x * cellSize + cellSize / 2f)
            + transform.up * (cell.y * cellSize + cellSize / 2f)
            + transform.forward * snapHeight;
            
        return cellCenter;
    }
    
    /// <summary>
    /// Check if a grid cell is valid (within bounds)
    /// </summary>
    public bool IsValidCell(Vector2Int cell)
    {
        return cell.x >= 0 && cell.x < columns && cell.y >= 0 && cell.y < rows;
    }
    
    /// <summary>
    /// Check if a cell is occupied (excluding a specific piece)
    /// </summary>
    public bool IsCellOccupied(Vector2Int cell, PuzzlePiece excludePiece = null)
    {
        if (!IsValidCell(cell)) return true;
        
        PuzzlePiece occupant = gridState[cell.x, cell.y];
        return occupant != null && occupant != excludePiece;
    }
    
    /// <summary>
    /// Try to place a piece on the grid
    /// </summary>
    public bool TryPlacePiece(PuzzlePiece piece, Vector3 worldPos)
    {
        Vector2Int cell = WorldToGrid(worldPos);
        
        // Check if placement is valid
        if (!IsValidCell(cell))
        {
            Debug.Log($"Cell {cell} is out of bounds");
            return false;
        }
        
        if (IsCellOccupied(cell, piece))
        {
            Debug.Log($"Cell {cell} is already occupied");
            return false;
        }
        
        // Remove piece from old cell if it was placed
        RemovePiece(piece);
        
        // Place piece in new cell
        gridState[cell.x, cell.y] = piece;
        piece.SetPlacedState(cell, GridToWorld(cell), -transform.forward);
        
        Debug.Log($"Placed piece at cell {cell}");
        
        // Check for completion
        CheckCompletion();
        
        return true;
    }
    
    /// <summary>
    /// Remove a piece from the grid
    /// </summary>
    public void RemovePiece(PuzzlePiece piece)
    {
        // Find and remove piece from grid
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                if (gridState[x, y] == piece)
                {
                    gridState[x, y] = null;
                    return;
                }
            }
        }
    }
    
    /// <summary>
    /// Check if puzzle is complete
    /// </summary>
    void CheckCompletion()
    {
        // Check if all pieces are placed
        int placedCount = 0;
        foreach (var piece in pieces)
        {
            if (piece.IsPlaced) placedCount++;
        }
        
        if (placedCount != pieces.Length) return;
        
        // Check if all pieces are in correct positions
        bool allCorrect = true;
        foreach (var piece in pieces)
        {
            if (!piece.IsInCorrectPosition())
            {
                allCorrect = false;
                break;
            }
        }
        
        if (allCorrect)
        {
            Debug.Log("Puzzle completed successfully!");
            OnPuzzleCompleted?.Invoke();
        }
        else
        {
            Debug.Log("Puzzle failed - pieces in wrong positions");
            OnPuzzleFailed?.Invoke();
            
            // Return all pieces to start
            foreach (var piece in pieces)
            {
                piece.ReturnToStart();
            }
        }
    }
    
    /// <summary>
    /// Reset the entire puzzle
    /// </summary>
    public void ResetPuzzle()
    {
        // Clear grid
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                gridState[x, y] = null;
            }
        }
        
        // Reset all pieces
        foreach (var piece in pieces)
        {
            piece.ReturnToStart();
        }
    }
    
    void OnDrawGizmos()
    {
        if (!showDebugGizmos) return;
        
        // Initialize if needed
        if (gridState == null) InitializeGrid();
        
        // Draw grid
        Gizmos.color = Color.cyan;
        
        for (int x = 0; x <= columns; x++)
        {
            Vector3 start = gridOrigin + transform.right * (x * cellSize);
            Vector3 end = start + transform.up * (rows * cellSize);
            Gizmos.DrawLine(start, end);
        }
        
        for (int y = 0; y <= rows; y++)
        {
            Vector3 start = gridOrigin + transform.up * (y * cellSize);
            Vector3 end = start + transform.right * (columns * cellSize);
            Gizmos.DrawLine(start, end);
        }
        
        // Draw cell centers
        Gizmos.color = Color.yellow;
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Vector3 center = GridToWorld(new Vector2Int(x, y));
                Gizmos.DrawWireSphere(center, cellSize * 0.1f);
            }
        }
    }
}
