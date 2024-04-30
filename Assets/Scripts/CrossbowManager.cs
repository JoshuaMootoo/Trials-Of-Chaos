using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowManager : MonoBehaviour
{
    private PlayerController player;
    public GameObject arrowPrefab;
    public Transform crossbowPos;
    public float arrowSpeed = 20;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
    }

    public void OnShoot()
    {
        var arrow = Instantiate(arrowPrefab, crossbowPos.position, player.transform.rotation);
        arrow.GetComponent<Rigidbody>().velocity = transform.forward * arrowSpeed;
        arrow.GetComponent<PlayerArrowController>().damage = player.damage;
    }
}
