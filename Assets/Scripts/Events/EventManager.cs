using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    #region GameState Events
    public delegate void GameStateAction();
    public  GameStateAction MainMenu;
    public  GameStateAction InGame;
    public  GameStateAction GameOver;
    public  GameStateAction EndGame;
    public  GameStateAction Battle;
    public  GameStateAction FirstStartGame;
    #endregion

    #region Battle Events
    public delegate void BattleStateActions();
    public BattleStateActions PlayerTurn;
    public BattleStateActions EnemyTurn;
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
    
    IEnumerator SubscribeAllEvents()
    {
        yield return new WaitForSeconds(.15f);
        #region GameState Events Subscribe
        // first start game
        FirstStartGame += AnimationManager.Instance.InGameAnimation;
        FirstStartGame += UIManager.Instance.PlayerUIUpdate;
        //ýn game state
        InGame += UIManager.Instance.PlayerBarBackParent;
        InGame += AnimationManager.Instance.InGameAnimation;
        InGame += UIManager.Instance.GoBackInGamePanelFromBattle;
        // battle state
        Battle += CameraController.Instance.ChangeCamera;
        Battle += AnimationManager.Instance.StartBattleAnimation;
        Battle += UIManager.Instance.EnemyUIUpdate;
        Battle += UIManager.Instance.GoBattlePanel;
        Battle += UIManager.Instance.PlayerBarChangeParent;
        #endregion

        #region BattleState Events Subscribe
        //enemy turn
        EnemyTurn += UIManager.Instance.CloseHitPanel;
        // player turn
        PlayerTurn += UIManager.Instance.OpenHitPanel;
        PlayerTurn += HitBarController.Instance.ResetHitBar;
        #endregion

        

    }

}
