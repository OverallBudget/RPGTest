using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;
    [SerializeField] int defense;
    [SerializeField] GameObject enemyType;

    private void Awake()
    {
        
    }
}
