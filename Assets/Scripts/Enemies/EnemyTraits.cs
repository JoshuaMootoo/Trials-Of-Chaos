using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyTraits", menuName = "EnemyTraits")]
public class EnemyTraits : ScriptableObject
{
    public string enemyName;

    public float health;
    public float damage;    
    public float speed;

    public int score;
}
