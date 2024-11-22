using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;
    int maxHP;
    int maxMP;
    public void SetHUD(PlayerScript player)
    {
        maxHP = player.getMaxHealth();
        int currentHP = player.getCurrentHealth();
        healthText.text = currentHP + " / " + maxHP;
        maxMP = player.getMaxMana();
        int currentMP = player.getCurrentMana();
        healthText.text = currentMP + " / " + maxMP;
    }

    public void setHP(int hp)
    {
        healthText.text = hp + " / " + maxHP;
    }

    public void setMP(int mp)
    {
        manaText.text = mp + " / " + maxMP;
    }
}
