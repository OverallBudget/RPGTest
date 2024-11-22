using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] OverworldPlayer player;
    [SerializeField] BattleSystem bSystem;
    public enum GameState { Overworld, Battle }
    GameState state;
    // Start is called before the first frame update
    void Start()
    {
        player.onBattle += StartBattle;
        state = GameState.Overworld;
    }

    void StartBattle()
    {
        state = GameState.Battle;
        bSystem.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.Overworld)
        {
            player.HandleUpdate();
        }
        else
        {
            bSystem.HandleUpdate();
        }
    }
}
