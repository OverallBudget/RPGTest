using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicScript
{
    public PlayerScript player; 
    public string attackName;
    public string attackType; // damage, heal, buff, debuff
    public string attackDescript;
    public string elementProperty; // fire? water? ice? none?
    public float attackDamage;
    public float attackCost; // mana
    public bool area; // area of effect?
}
