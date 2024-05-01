using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponDamage : MonoBehaviour
{
    PlayerController player;
    public float playerDamage;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Player Has Hit Enemy");
            collision.gameObject.GetComponent<EnemyBehavior>().TakeDamage(playerDamage);
        }
    }
}
