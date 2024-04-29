using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    private EnemyBehavior enemyBehavior;
    private PlayerController player;

    private void Start()
    {
        enemyBehavior = GetComponent<EnemyBehavior>();
        player = FindFirstObjectByType<PlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") player.TakeDamage(enemyBehavior.damage);
    }
}
