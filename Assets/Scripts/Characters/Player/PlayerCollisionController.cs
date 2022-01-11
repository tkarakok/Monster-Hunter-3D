using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCollisionController : Singleton<PlayerCollisionController>
{
    private EnemyController _enemyController;

    public EnemyController EnemyController { get => _enemyController; set => _enemyController = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            HitBarController.Instance.ResetHitBar();
            transform.DOMoveX(other.transform.position.x,.5f);
            EnemyController = other.gameObject.GetComponent<EnemyController>();
            StartCoroutine(ChangeState());

        }
        else if (other.CompareTag("Xp"))
        {
            
            PlayerController.Instance.EarnXp(40);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Diamond"))
        {
            PlayerController.Instance.Diamond++;
            UIManager.Instance.DiamondCounter();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("WizardRay"))
        {
            Destroy(other);
        }
        else if (other.CompareTag("Finish"))
        {
            PlayerPrefs.SetInt("Level",(LevelManager.Instance.CurrentLevel+ 1));
            StateManager.Instance.GameState = GameState.EndGame;
            EventManager.Instance.CheckGameStateEvent(StateManager.Instance.GameState);
        }

    }

    IEnumerator ChangeState()
    {
        yield return new WaitForSeconds(1);
        StateManager.Instance.GameState = GameState.Battle;
        EventManager.Instance.CheckGameStateEvent(StateManager.Instance.GameState);
    }

    
}
