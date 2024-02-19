using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class EnemyStats
{
    [Header("Enemy Type")]
    public bool isLightMelee;
    public bool isHeavyMelee;
    public bool isRanged;

    [Header("Health")]
    public float currentHealth;
    public float maxHealth;
    public float healthMultiplier = 1;

    [Header("Attack")]
    public float damage;
    public float flatDamage;
    public float damageMultiplier = 1;

    public float moveSpeed;
}

public class EnemyController : MonoBehaviour
{
    private PlayerController playerController;

    public EnemyStats stats;

    public float stopDistance = 2;


    private void Start()
    {
        playerController = PlayerController.Instance;
        InitializeStats();
    }

    private void Update()
    {
        FollowPlayer(playerController.transform);
    }

    private void InitializeStats()
    {
        stats.currentHealth = stats.maxHealth * stats.healthMultiplier;
        stats.damage = stats.flatDamage * stats.damageMultiplier;
    }

    private void Attack()
    {

    }

    private void FollowPlayer(Transform player)
    {
        //  Calcultates the vector direction that goes towards the player
        Vector3 directionOfPlayer = new Vector3(player.position.x - transform.position.x, 0f, player.position.z - transform.position.z);

        //  Calculates the distance between the enemy and the player
        float playerDistance = Vector3.Distance(transform.position, player.position);

        //  Moves the player in the calculated vector direction at the enemys move speed
        if (playerDistance > stopDistance) transform.Translate(directionOfPlayer * stats.moveSpeed * Time.deltaTime);
    }


    private void OnDeath(bool isOutOfBound)
    {
        if (isOutOfBound) 
        { 
            Respawn(); 
        }
    }

    private void Respawn()
    {

    }
}
