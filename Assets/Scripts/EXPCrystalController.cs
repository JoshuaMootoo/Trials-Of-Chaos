using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPCrystalController : MonoBehaviour
{
    public int xpAmount = 1;

    PlayerController player;
    Vector3 playerPos;

    

    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
    }
    private void FixedUpdate()
    {
        playerPos = player.transform.position;

        float playerDistance = Vector3.Distance(transform.position, player.transform.position);


        if (playerDistance <= 10) Vector3.MoveTowards(transform.position, player.transform.position, 1);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Has collided with Player, Player has gained " + xpAmount + " EXP");
            collision.transform.GetComponent<PlayerController>().GainExp(xpAmount);
            Destroy(gameObject);
        }
    }
}
