using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTileObject : MonoBehaviour
{
    [SerializeField] private GameObject[] Objects;
    [SerializeField] int randomObjectNum;

    private void Start()
    {
        randomObjectNum = Random.Range(0, Objects.Length);
        for (int i = 0; i < Objects.Length; i++)
        {
            if (i == randomObjectNum) Objects[i].SetActive(true);
            else Objects[i].SetActive(false);
        }
    }
}
