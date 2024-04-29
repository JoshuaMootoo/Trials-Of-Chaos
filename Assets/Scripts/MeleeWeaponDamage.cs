using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponDamage : MonoBehaviour
{
    PlayerController player;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && player.canAttack && player.AttackButtonPressed)
        {
            collision.gameObject.GetComponent<EnemyBehavior>().TakeDamage(player.damage);
        }
    }
}
