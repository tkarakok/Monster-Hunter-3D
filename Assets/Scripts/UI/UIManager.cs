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
    public Text playerMaxLevelText;
    public Image playerHpBar;
    public Image playerHpDamageBar;
    public Image xpBar;
    public Transform PlayerBar;
    public Transform PlayerParentBar;
    public Text diamondText;
    public Text endGameDiamondText;

    [Header("Enemy UI")]
    public Image enemySprite;
    public Text enemyName;
    public Text enemyLevelText;
    public Image enemyHpBar;

    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject inGamePanel;
    public GameObject battlePanel;
    public GameObject gameOverPanel;
    public GameObject hitPanel;
    public GameObject endGamePanel;


    #region Button Functions
    public void StartButton()
    {
        mainMenuPanel.SetActive(false);
        inGamePanel.SetActive(true);
        EventManager.Instance.FirstStartGame();
        StateManager.Instance.GameState = GameState.InGame;
    }

    public void RestartButton()
    {
        LevelManager.Instance.ChangeLevel("LEVEL " + LevelManager.Instance.CurrentLevel);
    }

    public void NextLevelButton()
    {
        LevelManager.Instance.GetLevelName();
        LevelManager.Instance.ChangeLevel("LEVEL " + LevelManager.Instance.CurrentLevel);
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
    public void GameOverPanel()
    {
        battlePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }
    public void EndGamePanel()
    {
        inGamePanel.SetActive(false);
        endGamePanel.SetActive(true);
    }
    #endregion

    #region Enemy UI
    public void EnemyUIUpdate()
    {
        EnemyController enemyController = PlayerCollisionController.Instance.EnemyController;
        EnemyImage(enemyController);
        EnemyName(enemyController);
        EnemyLevel(enemyController);
        EnemyHP(enemyController);
    }
    public void EnemyTakeDamageHp(EnemyController enemyController)
    {
        if (enemyController.EnemyType == EnemyType.enemy1)
        {
            enemyHpBar.fillAmount = enemyController.Hp / 100;
        }
        else if (enemyController.EnemyType == EnemyType.enemy2)
        {
            enemyHpBar.fillAmount = enemyController.Hp / 200;
        }
        else if (enemyController.EnemyType == EnemyType.enemy3)
        {
            enemyHpBar.fillAmount = enemyController.Hp / 300;
        }
        else if (enemyController.EnemyType == EnemyType.enemy4)
        {
            enemyHpBar.fillAmount = enemyController.Hp / 400;
        }
        else if (enemyController.EnemyType == EnemyType.enemy5)
        {
            enemyHpBar.fillAmount = enemyController.Hp / 500;
        }

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
        DiamondCounter();
    }
    public void PlayerLevel()
    {
        playerLevelText.text = PlayerController.Instance.Level.ToString();
    }
    public void PlayerHP()
    {
        playerHpBar.fillAmount = PlayerController.Instance.Hp / 100;
        StartCoroutine(PlayerTakeDamageHpBar());

    }
    public IEnumerator PlayerTakeDamageHpBar()
    {
        yield return new WaitForSeconds(.25f);
        playerHpDamageBar.fillAmount = PlayerController.Instance.Hp / 100;
    }
    public void PlayerXP()
    {
        if (PlayerController.Instance.Level == PlayerController.Max_Lvl)
        {
            xpBar.fillAmount = 0;
        }
        else
        {
            xpBar.fillAmount = PlayerController.Instance.Xp / 100;

        }
        
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
    public void DiamondCounter()
    {
        diamondText.text = PlayerController.Instance.Diamond.ToString();
    }
    public void EndGameDiamondCounter()
    {
        endGameDiamondText.text = diamondText.text;
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
