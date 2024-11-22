using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinorHeal : MagicScript
{
    public MinorHeal()
    {
        attackName = "Minor Heal";
        attackType = "Heal";
        attackDescript = "Heals an ally a small amount of HP.";
        elementProperty = "Holy";
        attackDamage = 3;
        attackCost = 2;
        area = false;
    }
}
