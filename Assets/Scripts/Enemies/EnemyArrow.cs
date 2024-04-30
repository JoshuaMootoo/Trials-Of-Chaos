using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour
{
    public float damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Level") Destroy(gameObject);
    }
}
