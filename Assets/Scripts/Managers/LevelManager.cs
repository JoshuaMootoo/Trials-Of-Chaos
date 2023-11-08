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
            int randomTile = Random.Range(0, TileTypes.Length);
            int randomRotations = Random.Range(0, rotations.Length);
            GameObject tile = Instantiate(TileTypes[randomTile], tilePos.position, Quaternion.Euler(0, rotations[randomRotations], 0));
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
                if (tiles.transform.position == centerTile.position + TilePos[0].position) newTiles[0] = tiles.transform;
                else if (tiles.transform.position == centerTile.position + TilePos[1].position) newTiles[1] = tiles.transform;
                else if (tiles.transform.position == centerTile.position + TilePos[2].position) newTiles[2] = tiles.transform;
                else if (tiles.transform.position == centerTile.position + TilePos[3].position) newTiles[3] = tiles.transform;
                else if (tiles.transform.position == centerTile.position + TilePos[5].position) newTiles[5] = tiles.transform;
                else if (tiles.transform.position == centerTile.position + TilePos[6].position) newTiles[6] = tiles.transform;
                else if (tiles.transform.position == centerTile.position + TilePos[7].position) newTiles[7] = tiles.transform;
                else if (tiles.transform.position == centerTile.position + TilePos[8].position) newTiles[8] = tiles.transform;
                else Destroy(tiles);
            }
            else newTiles[4] = tiles.transform;


            for (int i = 0; i < 9; i++)
            {
                currentTiles[i] = newTiles[i];
            }
        }
    }
}