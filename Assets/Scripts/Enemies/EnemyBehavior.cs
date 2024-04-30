using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    Animator anim;

    public EnemyTraits enemyTraits;

    private PlayerController player;
    private WaveManager waveManager;


    public float currentHealth;
    public float damage;
    public float speed;
    public float multiplier;

    [SerializeField] private bool isBoss;

    [SerializeField] private float stopDistance;
    [SerializeField] private float outOfBoundsDistance;

    public GameObject EXPCrystal;
    public GameObject HealthItem;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindFirstObjectByType<PlayerController>();
        waveManager = FindFirstObjectByType<WaveManager>();

        InitialiseEnemy();
    }


    private void InitialiseEnemy()
    {
        currentHealth = enemyTraits.health * multiplier;
        damage = enemyTraits.damage * multiplier;
        speed = enemyTraits.speed * multiplier;
    }

    private void FixedUpdate()
    {
        EnemyMovement();
    }

    private void EnemyMovement()
    {
        //  This Calculates the Distance Between the Enemy and the Player
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        //Debug.Log(playerDistance);

        //  This Calculates the Direction of the Player based on the Enemy Position
        Vector3 directionOfPlayer = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);
        //  makes the enemy move towards the player at a set Speed ("speed" float)
        if (playerDistance > stopDistance) transform.Translate(directionOfPlayer * speed * Time.fixedDeltaTime);

        //  This triggers the Death Function if the distance Between the Player Exceeds the outOfBoundsDistance
        if (playerDistance > outOfBoundsDistance && !isBoss) OnDeath(isBoss);
    }

    //------------------------------------------------------------------------------------
    //                                      To Be Called
    //------------------------------------------------------------------------------------

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log(enemyTraits.enemyName + " took " + damageAmount + " danage.");
        

        if (currentHealth <= 0) 
        {
            OnDeath(isBoss);
        } 
        else Debug.Log(enemyTraits.enemyName + " has " + currentHealth + " health remaining");
    }

    public void OnDeath(bool isBoss)
    {
        Debug.Log(enemyTraits.enemyName+ " has died");

        GameManager.Instance.killCount++;

        anim.SetBool("isDead", true);

        if (!isBoss)
        {
            waveManager.currentEnemyCount -= 1;
            Instantiate(EXPCrystal, transform.position, Quaternion.identity);
            float healthSpawnChance = Random.Range(0, 100);
            if (healthSpawnChance <= 90) Instantiate(HealthItem, transform.position, Quaternion.identity);
        }
        else GameManager.Instance.GameOver(false);
        StartCoroutine(WaitToDestroy());
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
