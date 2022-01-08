using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : Singleton<PlayerCollisionController>
{
    private GameObject _enemy;

    public GameObject Enemy { get => _enemy; set => _enemy = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            HitBarController.Instance.ResetHitBar();
            Enemy = other.gameObject;
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
            Destroy(other.gameObject);
        }
    }

    IEnumerator ChangeState()
    {
        yield return new WaitForSeconds(1);
        StateManager.Instance.GameState = GameState.Battle;
        EventManager.Instance.CheckGameStateEvent(StateManager.Instance.GameState);
    }

    
}
