using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;
    [SerializeField] int defense;
    [SerializeField] GameObject enemyType;
    [SerializeField] string enemyName;

    private void Awake()
    {
        
    }
}
