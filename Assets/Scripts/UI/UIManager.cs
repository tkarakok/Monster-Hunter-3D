using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Player UI")]
    public Image playerSprite;
    public Text playerName;
    public Text playerLevelText;
    public Image playerhpBar;
    public Image xpBar;
    public Transform PlayerBar;
    public Transform PlayerParentBar;


    [Header("Enemy UI")]
    public Image enemySprite;
    public Text enemyName;
    public Text enemyLevelText;
    public Image enemyHpBar;

    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject inGamePanel;
    public GameObject battlePanel;
    public GameObject hitPanel;


    #region Button Functions
    public void StartButton()
    {
        mainMenuPanel.SetActive(false);
        inGamePanel.SetActive(true);
        EventManager.Instance.FirstStartGame();
        StateManager.Instance.GameState = GameState.InGame;
    }
    #endregion

    #region Panel Change Function
    public void GoBattlePanel()
    {
        inGamePanel.SetActive(false);
        battlePanel.SetActive(true);
    }
    public void GoBackInGamePanelFromBattle()
    {
        inGamePanel.SetActive(true);
        battlePanel.SetActive(false);
    }
    #endregion

    #region Enemy UI
    public void EnemyUIUpdate()
    {
        EnemyController enemyController = PlayerCollisionController.Instance.Enemy.GetComponent<EnemyController>();
        EnemyImage(enemyController);
        EnemyName(enemyController);
        EnemyLevel(enemyController);
        EnemyHP(enemyController);
    }
    public void EnemyImage(EnemyController enemyController)
    {
        enemySprite.sprite = enemyController.EnemySprite;
    }
    public void EnemyName(EnemyController enemyController)
    {
        enemyName.text = enemyController.EnemyName;
    }
    public void EnemyLevel(EnemyController enemyController)
    {
        enemyLevelText.text = enemyController.EnemyLevel.ToString();
    }
    public void EnemyHP(EnemyController enemyController)
    {
        enemyHpBar.fillAmount = enemyController.Hp / 100;
    }
    #endregion

    #region Player UI 
    public void PlayerUIUpdate()
    {
        PlayerLevel();
        PlayerHP();
        PlayerXP();
        PlayerImage();
        PlayerName();
    }
    public void PlayerLevel()
    {
        playerLevelText.text = PlayerController.Instance.Level.ToString();
    }
    public void PlayerHP()
    {
        playerhpBar.fillAmount = PlayerController.Instance.Hp / 100;
    }
    public void PlayerXP()
    {
        xpBar.fillAmount = PlayerController.Instance.Xp / 100;
    }
    public void PlayerImage()
    {
        playerSprite.sprite = PlayerController.Instance.playerSprite;
    }
    public void PlayerName()
    {
        playerName.text = PlayerController.Instance.PlayerName;
    }
    public void PlayerBarChangeParent()
    {
        PlayerBar.SetParent(battlePanel.transform);
    }
    public void PlayerBarBackParent()
    {
        PlayerBar.SetParent(PlayerParentBar);
    }

    #endregion

    #region HitBar Open/Close
    public void OpenHitPanel()
    {
        if (!hitPanel.activeInHierarchy)
        {
            hitPanel.SetActive(true);
        }

    }
    public void CloseHitPanel()
    {
        hitPanel.SetActive(false);
    }
    #endregion
}
