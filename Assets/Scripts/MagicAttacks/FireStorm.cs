using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStorm : MagicScript
{
    public FireStorm()
    {
        attackName = "FireStorm";
        attackType = "Damage";
        attackDescript = "Cover the area with a small blaze.";
        elementProperty = "Fire";
        attackDamage = 2;
        attackCost = 5;
        area = true;
    }
}
