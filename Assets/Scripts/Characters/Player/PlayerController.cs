using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController> , ICharacter
{
    public const int Max_Lvl = 5;

    [SerializeField] private PlayerFields playerFields = null;

    private int _level;
    private float _hp;
    private float _hit;
    private float _xp;
    private int _bonus;

    private bool _alive = true;

    #region EnCapsulation
    public float Xp { get => _xp; set => _xp = value; }
    public int Bonus { get => _bonus; set => _bonus = value; }
    public int Level { get => _level; set => _level = value; }
    public float Hp { get => _hp; set => _hp = value; }
    public float Hit { get => _hit; set => _hit = value; }
    public bool Alive { get => _alive; set => _alive = value; }

    #endregion

    private void Start()
    {
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
        // player attac anim
        EnemyController enemyController = PlayerCollisionController.Instance.Enemy.GetComponent<EnemyController>();
        enemyController.TakeDamage(CalculateHit());
    }

    public void TakeDamage(float hit)
    {
        // damage anim
        Hp -= hit;
        Debug.Log(Hp);
        if (Hp < 0)
        {
            StateManager.Instance.GameState = GameState.GameOver;
        }
        else
        {
            StateManager.Instance.BattleState = BattleState.PlayerTurn;
        }
    }
    #endregion

    #region XP Functions

    public void LevelUp()
    {
        Level += 1;
        // level upgrade event
    }

    public void EarnXp(float xp)
    {
        Xp += xp;
        if (Xp >= 100)
        {
            Xp -= 100;
            LevelUp();
        }
    }

    #endregion


}
