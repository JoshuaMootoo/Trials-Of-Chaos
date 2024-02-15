using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }
    
    private const int NumTileTypes = 4;                                 // Number of Tile Types
    private const int TileDistance = 50;                                // Tile Distance between eachother
    private const int LoadDistance = 1;                                 // Number of Tiles to load in each direction from the Current Center Tile
    
    [Header("Tile Preset Variables")]
    public GameObject[] tileTypes = new GameObject[NumTileTypes];       // Array of Tile Type GameObjects
    private float[] possibleRotations = { 0, 90, 180, 270 };            // Array of Possible Tile Y Axis Rotations 

    private int gridSize = 3;                                           // Grid size 
    public GameObject[,] grid;

    private void Awake()
    {
        #region Instance - Awake
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
        #endregion

        InitializeGrid();
        PositionPlayerOnCenter();
    }

    // This is used Initalises the grid 
    private void InitializeGrid()
    {
        grid = new GameObject[gridSize, gridSize];
    }

    private void Start()
    {
        UpdateGrid();
    }
      
    private GameObject CreateTile(int x, int y)                         // Used to Instantiate a random tile on a position
    {
        int randomPlatform = Random.Range(0, tileTypes.Length);
        int randomRotations = Random.Range(0, possibleRotations.Length);
        Debug.Log(x + "," + y);
        return Instantiate(tileTypes[randomPlatform], 
                           new Vector3(x * TileDistance, 0, y * TileDistance), 
                           Quaternion.Euler(0, possibleRotations[randomRotations], 0));
    }

    private void DestroyTile(int x, int y)                              // Used to Destroy Tile GameObjects
    {
        if (grid[x, y] != null)
        {
            Destroy(grid[x, y]);
            grid[x, y] = null;
        }
    }

    public void UpdateGrid()
    {
        Debug.Log("Updating Grid...");
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                if (grid[i,j] == null)
                {
                    grid[i,j] = CreateTile(i, j);
                }
            }
        }
    }
    private void UpdateGridBasedOnPlayerPosition()
    {
        GameObject player = PlayerController.Instance.gameObject;

        if (player != null)
        {
            int playerX = Mathf.RoundToInt(player.transform.position.x / TileDistance);
            int playerZ = Mathf.RoundToInt(player.transform.position.z / TileDistance);

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (Mathf.Abs(playerX - i) > 1 || Mathf.Abs(playerZ - j) > 1)
                    {
                        DestroyTile(i, j);
                    }
                    else if (grid[i, j] == null)
                    {
                        grid[i, j] = CreateTile(i, j);
                    }
                }
            }
        }
    }

    private void PositionPlayerOnCenter()
    {
        // Looks for GameObject with "Player"
        GameObject player = PlayerController.Instance.gameObject;

        if (player != null)
        {
            int centerIndex = gridSize / 2;
            Vector3 centerPosition = new Vector3(centerIndex * TileDistance, 0, centerIndex * TileDistance);

            player.transform.position = centerPosition;
        }
        else
        {
            Debug.LogError("Player GameObject not found. Make sure the player object has the correct name.");
        }
    }

    public void PlayerHasMoved()
    {
        UpdateGridBasedOnPlayerPosition();
    }
}
