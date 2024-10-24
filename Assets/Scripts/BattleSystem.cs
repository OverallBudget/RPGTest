using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WIN, LOSE}
public class BattleSystem : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject enemy2;
    [SerializeField] GameObject enemy3;
    [SerializeField] GameObject enemy4;
    [SerializeField] TextMeshProUGUI actionDescript;
    [SerializeField] GameObject selector;
    [SerializeField] Transform playerLocation;
    int enemy;
    string enemy1Name;

    PlayerScript PlayerUnit;
    EnemyScript EnemyUnit;
    [SerializeField] Transform enemy1Location;
    [SerializeField] Transform enemy2Location;
    [SerializeField] Transform enemy3Location;
    [SerializeField] Transform enemy4Location;
    Vector2 pos1;
    Vector2 pos2;
    Vector2 pos3;
    Vector2 pos4;
    float upOffset = 50f;


    public BattleState state;

    public BattleHUD enemy1HUD;

    private void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }

    void SetupBattle()
    {
        GameObject PlayerGO = Instantiate(player, playerLocation);
        PlayerUnit = PlayerGO.GetComponent<PlayerScript>();
        GameObject EnemyGO = Instantiate(enemy1, enemy1Location);
        EnemyUnit = EnemyGO.GetComponent<EnemyScript>();
        enemy1Name = EnemyUnit.getName();
        //if (enemy2 != null) { Instantiate(enemy2, enemy2Location); } // keep it simple and have 1 for now
        //if (enemy3 != null) { Instantiate(enemy3, enemy3Location); }
        //if (enemy4 != null) { Instantiate(enemy4, enemy4Location); }

        enemy1HUD.SetHUD(EnemyUnit);
    }
}
