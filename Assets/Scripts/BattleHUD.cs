using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Slider healthSlider;
    [SerializeField] Transform position;

    private void Start()
    {
        
    }
    public void SetHUD(EnemyScript enemy)
    {
        nameText.text = enemy.getName();
        healthSlider.maxValue = enemy.getMaxHealth();
        healthSlider.value = enemy.getCurrentHealth();
        nameText.transform.position = position.position;
        healthSlider.transform.position = position.position;
    }

    void setHP(int hp)
    {
        healthSlider.value = hp;
    }
}
