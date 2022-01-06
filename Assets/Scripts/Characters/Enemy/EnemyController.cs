using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour , ICharacter
{
    [SerializeField] private EnemyFields enemyFields = null;

    private EnemyType _enemyType;
    private string _enemyName;
    private float _hp;
    private float _damage;

    private bool _alive = true;

    #region EnCapsulation
    public EnemyType EnemyType { get => _enemyType; set => _enemyType = value; }
    public string EnemyName { get => _enemyName; set => _enemyName = value; }
    public float Hp { get => _hp; set => _hp = value; }
    public float Damage { get => _damage; set => _damage = value; }
    public bool Alive { get => _alive; set => _alive = value; }

    
    #endregion

    void Start()
    {
        EnemyType = enemyFields.enemyType;
        EnemyName = enemyFields.enemyName;
        Hp = enemyFields.hp;
        Damage = enemyFields.damage;
        transform.localScale = enemyFields.scale;
    }

    #region Damage Functions

    public void InflictDamage()
    {
        // enemy attack anim
        PlayerController.Instance.TakeDamage(Damage);
    }

    public void TakeDamage(float hit)
    {
        // enemt damage anim
        Hp -= hit;
        if (Hp < 0)
        {
            Destroy(gameObject);
            Alive = false;
            StateManager.Instance.GameState = GameState.InGame;
        }
        else
        {
            StateManager.Instance.BattleState = BattleState.EnemyTurn;
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        
            yield return new WaitForSeconds(1.5f);
            InflictDamage();
        
    }

    #endregion

}
