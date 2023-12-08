using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public Transform centerTile;

    public GameObject[] TileTypes = new GameObject[4];
    [SerializeField] float[] rotations = new float[4];
    public Transform[] TilePos = new Transform[9];
    public Transform[] currentTiles = new Transform[9];
    public Transform[] newTiles = new Transform[9];


    private void Start()
    {
        foreach (Transform tilePos in TilePos)
        {
            CreateTiles(tilePos);
        }
    }



    public void DetroyTiles()
    {
        foreach (GameObject tiles in GameObject.FindGameObjectsWithTag("Tile"))
        {
            if (centerTile == null) return;
            else

            if (tiles.transform != centerTile)
            {
                Vector3[] newTilePos = new Vector3[TilePos.Length];
                for (int i = 0; i < TilePos.Length; i++) newTilePos[i] = centerTile.position + TilePos[i].position;
                
                if      (tiles.transform.position == newTilePos[0]) newTiles[0] = tiles.transform;
                else if (tiles.transform.position == newTilePos[1]) newTiles[1] = tiles.transform;
                else if (tiles.transform.position == newTilePos[2]) newTiles[2] = tiles.transform;
                else if (tiles.transform.position == newTilePos[3]) newTiles[3] = tiles.transform;
                else if (tiles.transform.position == newTilePos[5]) newTiles[5] = tiles.transform;
                else if (tiles.transform.position == newTilePos[6]) newTiles[6] = tiles.transform;
                else if (tiles.transform.position == newTilePos[7]) newTiles[7] = tiles.transform;
                else if (tiles.transform.position == newTilePos[8]) newTiles[8] = tiles.transform;
                else Destroy(tiles);


            }
            else newTiles[4] = tiles.transform;


            for (int i = 0; i < 9; i++)
            {
                currentTiles[i] = newTiles[i];
            }
        }
    }

    public void CreateTiles(Transform _TilePos)
    {
        int randomTile = Random.Range(0, TileTypes.Length);
        int randomRotations = Random.Range(0, rotations.Length);
        GameObject tile = Instantiate(TileTypes[randomTile], _TilePos.position, Quaternion.Euler(0, rotations[randomRotations], 0));
        tile.transform.SetParent(_TilePos);
    }

    public void GridSetter()
    {
        Vector3[] test = new Vector3[TilePos.Length];
        for(int i = 0; i < TilePos.Length; i++) test[i] = centerTile.position + TilePos[i].position;

        foreach (GameObject tiles in GameObject.FindGameObjectsWithTag("Tile"))
        {

        }
    }
}