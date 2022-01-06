using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    #region GameState Events
    public delegate void GameStateAction();
    public event GameStateAction MainMenu;
    public event GameStateAction InGame;
    public event GameStateAction GameOver;
    public event GameStateAction EndGame;
    public event GameStateAction Battle;
    #endregion

    #region Battle Events
    /*
    public delegate void InBattleActions();
    public event InBattleActions PlayerTurn;
    public event InBattleActions EnemyTurn;
    */
    #endregion

    private void Start()
    {
        StartCoroutine(SubscribeAllEvents());
    }
    


    public void CheckGameStateEvent(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MainMenu:
                break;
            case GameState.InGame:
                InGame();
                break;
            case GameState.EndGame:
                break;
            case GameState.GameOver:
                break;
            case GameState.Battle:
                Battle();
                break;
            default:
                break;
        }
    }
    /*
    public void CheckBattleStateEvent(BattleState battleState)
    {
        switch (battleState)
        {
            case BattleState.PlayerTurn:
                PlayerTurn();
                break;
            case BattleState.EnemyTurn:
                EnemyTurn();
                break;
            default:
                break;
        }
    }
    */
    IEnumerator SubscribeAllEvents()
    {
        yield return new WaitForSeconds(.15f);
        #region GameState Events Subscribe
        //ýn game state
        InGame += UIManager.Instance.PlayerUIUpdate;
        InGame += UIManager.Instance.PlayerBarBackParent;
        InGame += UIManager.Instance.GoBackInGamePanelFromBattle;
        // battle state
        Battle += UIManager.Instance.EnemyUIUpdate;
        Battle += UIManager.Instance.GoBattlePanel;
        Battle += UIManager.Instance.PlayerBarChangeParent;
        #endregion
        /*
        #region BattleState Events Subscribe
        // player turn
        
        PlayerTurn += HitBarController.Instance.ResetHitBar;
        PlayerTurn += UIManager.Instance.PlayerHP;
        PlayerTurn += UIManager.Instance.PlayerXP;
        //ENEMY TURN
        EnemyTurn += UIManager.Instance.EnemyUIUpdate;
        #endregion*/
    }

}
