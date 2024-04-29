using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // Needed for ToList()

public class GridManager : MonoBehaviour
{
    private const int NumTileTypes = 4;                                 // Number of Tile Types
    private const int TileDistance = 50;                                // Tile Distance between each other
    private const int LoadDistance = 1;                                 // Number of Tiles to load in each direction from the Current Center Tile
    private const int startHeight = 3;

    [Header("Tile Preset Variables")]
    public Material[] tileSetMats = new Material[3];
    public GameObject[] tileTypes = new GameObject[NumTileTypes];       // Array of Tile Type GameObjects
    private float[] possibleRotations = { 0, 90, 180, 270 };            // Array of Possible Tile Y Axis Rotations 

    private int gridSize = 3;                                           // Grid size 
    public GameObject[,] grid;

    private Dictionary<Vector2Int, GameObject> loadedTiles = new Dictionary<Vector2Int, GameObject>();
    private GameObject player;

    private void Awake()
    {
        // Looks for GameObject with "Player"
        player = FindFirstObjectByType<PlayerController>().gameObject;

        InitializeGrid();
        PositionPlayerOnCenter();
    }

    // This is used Initialises the grid 
    private void InitializeGrid()
    {
        grid = new GameObject[gridSize, gridSize];
    }

    private void Update()
    {
        UpdateGridBasedOnPlayerPosition();
    }

    private void UpdateGridBasedOnPlayerPosition()                  // Used to update grid whenever the player moves onto another GridTile
    {
        if (player != null)
        {
            Vector2Int playerGridPos = GetGridPosition(player.transform.position);

            // Load new tiles
            for (int i = -LoadDistance; i <= LoadDistance; i++)
            {
                for (int j = -LoadDistance; j <= LoadDistance; j++)
                {
                    Vector2Int tilePos = new Vector2Int(playerGridPos.x + i, playerGridPos.y + j);

                    if (!loadedTiles.ContainsKey(tilePos))
                    {
                        loadedTiles[tilePos] = CreateTile(tilePos.x, tilePos.y);
                    }
                }
            }

            // Unload tiles outside load distance
            List<Vector2Int> tilesToRemove = new List<Vector2Int>();
            foreach (var loadedTilePos in loadedTiles.Keys.ToList())
            {
                if (Mathf.Abs(loadedTilePos.x - playerGridPos.x) > LoadDistance || Mathf.Abs(loadedTilePos.y - playerGridPos.y) > LoadDistance)
                {
                    DestroyTile(loadedTilePos.x, loadedTilePos.y);
                    tilesToRemove.Add(loadedTilePos);
                }
            }

            foreach (var tilePosToRemove in tilesToRemove)
            {
                loadedTiles.Remove(tilePosToRemove);
            }
        }
    }

    private void PositionPlayerOnCenter()   // Sets the player's initial position to be the initial center tile - used in the Awake Function
    {
        if (player != null)
        {
            int centerIndex = gridSize / 2;
            Vector3 centerPosition = new Vector3(centerIndex * TileDistance, startHeight, centerIndex * TileDistance);

            player.transform.position = centerPosition;
        }
        else Debug.LogError("Player GameObject not found. Make sure the player object has the correct name.");
    }

    private GameObject CreateTile(int x, int y)                         // Used to Instantiate a random tile at a position
    {
        int randomPlatform = Random.Range(0, tileTypes.Length);
        int randomRotations = Random.Range(0, possibleRotations.Length);
        // Debug.Log(x + "," + y);
        GameObject tile = Instantiate(tileTypes[randomPlatform], new Vector3(x * TileDistance, 0, y * TileDistance), Quaternion.Euler(0, possibleRotations[randomRotations], 0));
        return tile;
    }

    private void DestroyTile(int x, int y)                              // Used to Destroy Tile Game Objects
    {
        if (loadedTiles.ContainsKey(new Vector2Int(x, y)))
        {
            Destroy(loadedTiles[new Vector2Int(x, y)]);
            loadedTiles.Remove(new Vector2Int(x, y));
        }
    }

    private Vector2Int GetGridPosition(Vector3 worldPosition)           // Used to get the Grid's Position in World Space using the player's position
    {
        int x = Mathf.RoundToInt(worldPosition.x / TileDistance);
        int y = Mathf.RoundToInt(worldPosition.z / TileDistance);
        return new Vector2Int(x, y);
    }
}
