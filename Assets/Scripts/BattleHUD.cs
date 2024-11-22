using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Slider healthSlider;
    public TextMeshProUGUI healthText;
    [SerializeField] Transform position;
    int maxHP;
    public void SetHUD(EnemyScript enemy)
    {
        maxHP = enemy.getMaxHealth();
        int currentHP = enemy.getCurrentHealth();
        healthSlider.maxValue = maxHP;
        healthSlider.value = currentHP;
        healthText.text = currentHP + "/" + maxHP;
    }

    public void setHP(int hp)
    {
        //healthSlider.value = hp;
        healthText.text = hp + "/" + maxHP;
    }

    public void hideText()
    {
        healthText.gameObject.SetActive(false);
    }
}
