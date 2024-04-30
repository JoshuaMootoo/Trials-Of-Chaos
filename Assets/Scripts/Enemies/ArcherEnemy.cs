using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherEnemy : MonoBehaviour
{
    EnemyBehavior enemy;
    public float attackTimer = 5;
    public float currentAttackTimer;
    public GameObject arrowPrefab;
    public Transform crossbowPos;
    public float arrowSpeed = 20;

    private void Start()
    {
        enemy = GetComponent<EnemyBehavior>();
        currentAttackTimer = attackTimer;
    }
    private void Update()
    {
        currentAttackTimer -= Time.deltaTime;
        if (currentAttackTimer < 0)
        {
            OnShoot();
            currentAttackTimer = attackTimer;
        }
    }
    public void OnShoot()
    {
        var arrow = Instantiate(arrowPrefab, crossbowPos.position, enemy.transform.rotation);
        arrow.GetComponent<Rigidbody>().velocity = transform.forward * arrowSpeed;
        arrow.GetComponent<EnemyArrow>().damage = enemy.damage;
    }
}
