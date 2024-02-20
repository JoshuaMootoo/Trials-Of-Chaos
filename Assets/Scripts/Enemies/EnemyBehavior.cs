using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public EnemyTraits enemyTraits;

    private PlayerController player;
    private WaveManager waveManager;


    public float currentHealth;
    public float damage;
    public float speed;

    [SerializeField] private bool isBoss;

    [SerializeField] private float stopDistance;
    [SerializeField] private float outOfBoundsDistance;

    private void Start()
    {
        player = PlayerController.Instance;
        waveManager = WaveManager.instance;

        InitialiseEnemy();
    }

    private void InitialiseEnemy()
    {
        currentHealth = enemyTraits.maxHealth * enemyTraits.healthMultiplier;
        damage = enemyTraits.baseDamage * enemyTraits.damageMultiplier;
        speed = enemyTraits.baseSpeed * enemyTraits.speedMultiplier;
    }

    private void FixedUpdate()
    {
        EnemyMovement();
    }

    private void EnemyMovement()
    {
        //  This Calculates the Distance Between the Enemy and the Player
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);

        //  This Calculates the Direction of the Player based on the Enemy Position
        Vector3 directionOfPlayer = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);
        //  makes the enemy move towards the player at a set Speed ("speed" float)
        if (playerDistance > stopDistance) transform.Translate(directionOfPlayer * speed * Time.fixedDeltaTime);

        //  This triggers the Death Function if the distance Between the Player Exceeds the outOfBoundsDistance
        if (playerDistance > outOfBoundsDistance) OnDeath(isBoss);
    }

    //------------------------------------------------------------------------------------
    //                                      To Be Called
    //------------------------------------------------------------------------------------

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0) 
        {
            OnDeath(isBoss);
        }
    }

    public void OnDeath(bool isBoss)
    {
        if (!isBoss) waveManager.currentEnemyCount -= 1;
        Destroy(gameObject);
    }
}
