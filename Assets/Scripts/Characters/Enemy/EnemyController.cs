using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour, ICharacter
{
    [SerializeField] private EnemyFields enemyFields = null;

    private EnemyType _enemyType;
    private Sprite _enemySprite;
    private string _enemyName;
    private float _hp;
    private float _damage;
    private int _enemyLevel;

    private bool _alive = true;
    private Animator _animator;

    #region EnCapsulation
    public EnemyType EnemyType { get => _enemyType; set => _enemyType = value; }
    public string EnemyName { get => _enemyName; set => _enemyName = value; }
    public float Hp { get => _hp; set => _hp = value; }
    public float Damage { get => _damage; set => _damage = value; }
    public bool Alive { get => _alive; set => _alive = value; }
    public Sprite EnemySprite { get => _enemySprite; set => _enemySprite = value; }
    public int EnemyLevel { get => _enemyLevel; set => _enemyLevel = value; }
    public Animator Animator { get => _animator; set => _animator = value; }


    #endregion

    void Start()
    {
        
        Animator = GetComponent<Animator>();
        EnemySprite = enemyFields.enemySprite;
        EnemyType = enemyFields.enemyType;
        EnemyName = enemyFields.enemyName;
        Hp = enemyFields.hp;
        Damage = enemyFields.damage;
        EnemyLevel = enemyFields.level;
        transform.localScale = enemyFields.scale;
    }

    #region Damage Functions
    public void InflictDamage()
    {
        PlayerController.Instance.TakeDamage(Damage);
    }

    public void TakeDamage(float hit)
    {
        StartCoroutine(EnemyTakeDamageAnimation());
        Hp -= hit;
        UIManager.Instance.EnemyUIUpdate();
        if (Hp <= 0)
        {
            Animator.SetBool("Die", true);
            Alive = false;
            StateManager.Instance.GameState = GameState.InGame;
            EventManager.Instance.CheckGameStateEvent(StateManager.Instance.GameState);
        }
        else
        {
            StartCoroutine(EnemyAttackAnimation());
        }
    }

    #endregion
    IEnumerator EnemyAttackAnimation()
    {
        yield return new WaitForSeconds(1.5f);
        Animator.SetBool("Attack", true);
        InflictDamage();
        yield return new WaitForSeconds(.5f);
        Animator.SetBool("Attack", false);
    }
    IEnumerator EnemyTakeDamageAnimation()
    {
        yield return new WaitForSeconds(.5f);
        Animator.SetBool("TakeDamage", true);
        yield return new WaitForSeconds(.5f);
        Animator.SetBool("TakeDamage", false);
    }

}
