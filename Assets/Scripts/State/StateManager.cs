using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : Singleton<StateManager>
{
    [SerializeField] GameState _gameState;
    [SerializeField] BattleState _battleState;

    public GameState GameState { get => _gameState; set => _gameState = value; }
    public BattleState BattleState { get => _battleState; set => _battleState = value; }


    void Start()
    {
        GameState = GameState.MainMenu;
        BattleState = BattleState.PlayerTurn;
    }
}
