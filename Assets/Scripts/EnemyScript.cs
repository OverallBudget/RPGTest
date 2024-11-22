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

    public bool TakeDamage(int damage) // return true on death, false if still living
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
