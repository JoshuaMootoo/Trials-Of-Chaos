using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class BruteEnemy : MonoBehaviour
{
    private EnemyBehavior enemyBehavior;
    private PlayerController player;
    public float playerKnockback = 15;

    private void Start()
    {
        enemyBehavior = GetComponent<EnemyBehavior>();
        player = FindFirstObjectByType<PlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.TakeDamage(enemyBehavior.damage);
            player.GetComponent<Rigidbody>().velocity = transform.forward * playerKnockback;
        }
    }
}
