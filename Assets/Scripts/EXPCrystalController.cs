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

        Vector3 directionOfPlayer = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);

        if (playerDistance <= 5) transform.Translate(directionOfPlayer * 10 * Time.fixedDeltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.GetComponent<PlayerController>().GainExp(xpAmount);
        }
    }
}
