using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyTraits", menuName = "EnemyTraits")]
public class EnemyTraits : ScriptableObject
{
    public string enemyName;

    public float maxHealth;
    public float healthMultiplier = 1;

    public float baseDamage;
    public float damageMultiplier = 1;
    
    public float baseSpeed;
    public float speedMultiplier = 1;
}
