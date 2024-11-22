using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] int currentHealth;
    [SerializeField] int defense;
    [SerializeField] int attack;
    [SerializeField] int maxMP;
    [SerializeField] int currentMP;

    public int getMaxHealth() { return maxHealth; }
    public int getCurrentHealth() { return currentHealth; }
    public int getMaxMana() { return maxMP; }
    public int getCurrentMana() { return currentMP; }
    public int getDefense() { return defense; }
    public int getAttack() { return attack; }

    public List<MagicScript> MagicAttacks = new List<MagicScript>();
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
    
    public void setHP(int hp)
    {
        currentHealth = hp;
    }

    public void changeMana(int cost)
    {
        currentMP -= cost;
    }

}
