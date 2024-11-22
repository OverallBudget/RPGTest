using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShrapnel : MagicScript
{
    public IceShrapnel()
    {
        attackName = "Ice Shrapnel";
        attackType = "Damage";
        attackDescript = "Send pieces of sharp ice at the enemy.";
        elementProperty = "Ice";
        attackDamage = 4;
        attackCost = 2;
        area = false;
    }
}
