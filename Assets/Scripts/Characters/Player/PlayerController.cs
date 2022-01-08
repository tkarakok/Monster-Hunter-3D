using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Singleton<PlayerController>, ICharacter
{
    public const int Max_Lvl = 5;

    [Header("Player Fields")]
    [SerializeField] private PlayerFields playerFields = null;


    [Header("Animation")]
    public Animator animator;

    [HideInInspector]
    public Sprite playerSprite;
    private string _playerName;
    private int _level;
    private float _hp;
    private float _hit;
    private float _xp;
    private int _bonus;

    private bool _alive = true;

    private int _diamond;

    #region EnCapsulation
    public float Xp { get => _xp; set => _xp = value; }
    public int Bonus { get => _bonus; set => _bonus = value; }
    public int Level { get => _level; set => _level = value; }
    public float Hp { get => _hp; set => _hp = value; }
    public float Hit { get => _hit; set => _hit = value; }
    public bool Alive { get => _alive; set => _alive = value; }
    public string PlayerName { get => _playerName; set => _playerName = value; }
    public int Diamond { get => _diamond; set => _diamond = value; }

    #endregion

    private void Start()
    {
        Diamond = PlayerPrefs.GetInt("Diamond");
        playerSprite = playerFields.playerSprite;
        PlayerName = playerFields.playerName;
        Level = playerFields.level;
        Hp = playerFields.hp;
        Hit = playerFields.hit;
        Xp = playerFields.xp;
        Bonus = playerFields.bonus;
    }


    #region Damage Functions

    public float CalculateHit()
    {
        Hit *= HitBarController.Instance.CalculateMultiplier(HitBarController.Instance.Multiplier);
        return Hit;
    }

    public void InflictDamage()
    {
        AnimationManager.Instance.StartPlayerAttackAnimation();
        EnemyController enemyController = PlayerCollisionController.Instance.Enemy.GetComponent<EnemyController>();
        enemyController.TakeDamage(CalculateHit());
        EarnXp(CalculateHit() / 2);
        
    }

    public void TakeDamage(float hit)
    {
        Hp -= hit;
        AnimationManager.Instance.StartPlayerTakeDamageAnimation();
        UIManager.Instance.PlayerHP();
        if (Hp <= 0)
        {
            Die();
            // event
            
        }
        else
        {
            StateManager.Instance.BattleState = BattleState.PlayerTurn;
            EventManager.Instance.CheckBattleStateEvent(StateManager.Instance.BattleState);
        }
    }

    void Die()
    {
        StateManager.Instance.GameState = GameState.GameOver;
        animator.SetBool("Die", true);
    }
    #endregion

    #region XP Functions

    public void LevelUp()
    {
        Level += 1;
        Hit = 200;
        transform.localScale += new Vector3(.25f,.25f,.25f);
        UIManager.Instance.PlayerUIUpdate();
    }

    public void EarnXp(float xp)
    {
        Xp += xp;
        if (Xp >= 100)
        {
            Xp -= 100;
            LevelUp();
        }
        else
        {
            UIManager.Instance.PlayerXP();
        }
    }

    #endregion


}
