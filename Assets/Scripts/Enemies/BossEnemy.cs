using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    EnemyBehavior enemy;
    Animator anim;

    [SerializeField] private GameObject SkeletonKnight;
    [SerializeField] private Transform throwHand;

    public int spawnCount = 0;
    [SerializeField] private int totalSpawns = 3;

    [SerializeField] private float storedSpeed;
    [SerializeField] private float throwForce;
    public float spawnMultiplier;

    private void Start()
    {
        enemy = GetComponent<EnemyBehavior>();
        anim = GetComponent<Animator>();
        
        storedSpeed = enemy.enemyTraits.speed * spawnMultiplier;
    }

    private void Update()
    {
        StartCoroutine(SpawnThrownEnemy());
        if (spawnCount >= totalSpawns)
        {
            anim.SetBool("isThrowing", false);
            enemy.speed = storedSpeed;
        }
        else
            enemy.speed = 0;
    }

    IEnumerator SpawnThrownEnemy()
    {
        yield return new WaitForSeconds(2);
        while (spawnCount < totalSpawns)
        {
            anim.SetBool("isThrowing", true);
            SpawnEnemy(); 
            yield return new WaitForSeconds(2);
        }         
    }

    private void SpawnEnemy()
    {
        var enemyClone = Instantiate(SkeletonKnight, throwHand.position, Quaternion.identity);
        enemyClone.GetComponent<EnemyBehavior>().multiplier = spawnMultiplier;
        enemyClone.GetComponent<EnemyBehavior>().spawnedFromBoss = true;
        enemyClone.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce + transform.up * throwForce, ForceMode.Impulse);
        spawnCount++;
    }
}
