using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;
    [SerializeField] int defense;
    [SerializeField] int damage;
    [SerializeField] GameObject enemyType;
    [SerializeField] string enemyName;

    public string getName() { return enemyName; }
    public int getMaxHealth() { return maxHealth; }
    public int getCurrentHealth() { return currentHealth; }
    public int getDefense() { return defense; }
    public int getDamage() {  return damage; }

    private void Awake()
    {
        
    }
}
