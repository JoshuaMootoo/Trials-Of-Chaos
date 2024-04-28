using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyStats
{
    [Header("Enemy Type")]
    public bool isLightMelee;
    public bool isHeavyMelee;
    public bool isRanged;
    public bool isBoss;

    [Header("Health")]
    public float currentHealth;
    public float healthMultiplier = 1;
    public float maxHealth;

    [Header("Attack")]
    public float damage;
    public float damageMultiplier = 1;
    public float flatDamage;

    [Header("Move Speed")]
    public float moveSpeed;
    public float moveSpeedMultiplier = 1;
    public float flatMoveSpeed;
}

public class EnemyController : MonoBehaviour
{
    private PlayerController player;

    public EnemyStats stats;

    [SerializeField] private float stopDistance = 2;
    [SerializeField] private float outOfBoundDistance = 75;


    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
        InitializeStats();
    }

    private void FixedUpdate()
    {
        EnemyPosition();
    }

    private void InitializeStats()
    {
        stats.currentHealth = stats.maxHealth * stats.healthMultiplier;
        stats.damage = stats.flatDamage * stats.damageMultiplier;
        stats.moveSpeed = stats.flatMoveSpeed * stats.moveSpeedMultiplier;
    }

    private void Attack()
    {

    }

    private void EnemyPosition()
    {
        //  Calculates the distance between the enemy and the player
        float playerDistance = UnityEngine.Vector3.Distance(transform.position, player.transform.position);

        FollowPlayer(playerDistance);
        OutOfBound(playerDistance);
    }

    private void FollowPlayer(float playerDistance)
    {
        //  Calcultates the vector direction that goes towards the player
        UnityEngine.Vector3 directionOfPlayer = new UnityEngine.Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);

        //  Moves the player in the calculated vector direction at the enemys move speed
        if (playerDistance > stopDistance) transform.Translate(directionOfPlayer * stats.moveSpeed * Time.fixedDeltaTime);
    }

    private void OutOfBound(float playerDistance)
    {
        if (playerDistance > outOfBoundDistance)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {

    }

}

