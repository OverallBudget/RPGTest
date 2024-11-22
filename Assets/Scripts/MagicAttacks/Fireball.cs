using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MagicScript
{
    public Fireball()
    {
        attackName = "Fireball";
        attackType = "Damage";
        attackDescript = "Lob a ball of fire at the enemy.";
        elementProperty = "Fire";
        attackDamage = 4;
        attackCost = 2;
        area = false;
    }
}
