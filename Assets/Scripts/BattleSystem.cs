using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    bool fighting;

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

    private void Start()
    {

        //if (enemy1 != null)
        //{
        //    Vector2 v = new Vector2(enemy1.transform.position.x, enemy1.transform.position.y + upOffset);
        //    pos1 = v;
        //}

        //if (enemy2 != null)
        //{
        //    Vector2 v = new Vector2(enemy2.transform.position.x, enemy2.transform.position.y + upOffset);
        //    pos2 = v;
        //}

        //if (enemy3 != null)
        //{
        //    Vector2 v = new Vector2(enemy3.transform.position.x, enemy3.transform.position.y + upOffset);
        //    pos3 = v;
        //}

        //if (enemy4 != null)
        //{
        //    Vector2 v = new Vector2(enemy4.transform.position.x, enemy4.transform.position.y + upOffset);
        //    enemy4 = new GameObject { hideFlags = HideFlags.HideAndDontSave };
        //    pos4 = v;
        //}

        state = BattleState.START;
        SetupBattle();
    }

    void SetupBattle()
    {
        Instantiate(player, playerLocation);
        if (enemy1 != null) { Instantiate(enemy1, enemy1Location); }
        if (enemy2 != null) { Instantiate(enemy2, enemy2Location); }
        if (enemy3 != null) { Instantiate(enemy3, enemy3Location); }
        if (enemy4 != null) { Instantiate(enemy4, enemy4Location); }
        
    }
}
