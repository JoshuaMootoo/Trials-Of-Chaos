using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerArrowController : MonoBehaviour
{
    private PlayerController player;
    public float damage;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerController>();
        damage = player.damage;
    }
    private void Update()
    {
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        if (playerDistance > 30) Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy") 
        { 
            collision.transform.GetComponent<EnemyBehavior>().TakeDamage(damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Level") Destroy(gameObject);
    }
}
