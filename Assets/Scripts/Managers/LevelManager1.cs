using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager1 : MonoBehaviour
{
    #region Instance
    public static LevelManager1 Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    #endregion
    
    [Header("Tile Preset Variables")]
    public GameObject[] TileTypes = new GameObject[4];
    [SerializeField] float[] rotations = new float[4];

    [Header("Tile Gameplay Variables")]
    public Transform centerTile;

    
    public Transform[] TilePos = new Transform[9];
    public Transform[] currentTiles = new Transform[9];
    public Transform[] newTiles = new Transform[9];


   
    private void Start()
    {
        foreach (Transform tilePos in TilePos) CreateTiles(tilePos.position);
    }

    #region Create Tile
    public void CreateTiles(Vector3 _tilePos) // Used to Instantiate a random tile on a position (Vector3) 
    {
        int randomTile = Random.Range(0, TileTypes.Length);
        int randomRotations = Random.Range(0, rotations.Length);
        GameObject tile = Instantiate(TileTypes[randomTile], _tilePos, Quaternion.Euler(0, rotations[randomRotations], 0));
    }
    #endregion


    public void DetroyTiles()
    {
        foreach (GameObject tiles in GameObject.FindGameObjectsWithTag("Tile"))
        {
            if (centerTile == null) return;
            else
            if (tiles.transform != centerTile)
            {
                if (tiles.transform.position == centerTile.position + TilePos[0].position) newTiles[0] = tiles.transform;
                else if (tiles.transform.position == centerTile.position + TilePos[1].position) newTiles[1] = tiles.transform;
                else if (tiles.transform.position == centerTile.position + TilePos[2].position) newTiles[2] = tiles.transform;
                else if (tiles.transform.position == centerTile.position + TilePos[3].position) newTiles[3] = tiles.transform;
                else if (tiles.transform.position == centerTile.position + TilePos[5].position) newTiles[5] = tiles.transform;
                else if (tiles.transform.position == centerTile.position + TilePos[6].position) newTiles[6] = tiles.transform;
                else if (tiles.transform.position == centerTile.position + TilePos[7].position) newTiles[7] = tiles.transform;
                else if (tiles.transform.position == centerTile.position + TilePos[8].position) newTiles[8] = tiles.transform;
                else tiles.SetActive(false);//Destroy(tiles);
            }
            else newTiles[4] = tiles.transform;


            //for (int i = 0; i < 9; i++)
            //{
            //    currentTiles[i] = newTiles[i];
            //}
        }
    }
}