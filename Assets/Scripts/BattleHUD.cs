using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Slider healthSlider;

    public void SetHUD(EnemyScript enemy)
    {
        nameText.text = enemy.getName();
        healthSlider.maxValue = enemy.getMaxHealth();
        healthSlider.value = enemy.getCurrentHealth();
    }

    void setHP(int hp)
    {
        healthSlider.value = hp;
    }
}
