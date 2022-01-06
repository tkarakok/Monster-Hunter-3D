using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : Singleton<PlayerCollisionController>
{
    [SerializeField] private GameObject _enemy;

    public GameObject Enemy { get => _enemy; set => _enemy = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy = other.gameObject;
            StartCoroutine(AnimatorBattle());
            StartCoroutine(ChangeState());
        }
    }

    IEnumerator ChangeState()
    {
        yield return new WaitForSeconds(1);
        StateManager.Instance.GameState = GameState.Battle;
    }

    IEnumerator AnimatorBattle()
    {
        PlayerController.Instance.animator.SetBool("InBattle", true);
        PlayerController.Instance.animator.SetBool("InGame", false);
        yield return new WaitForSeconds(1);
        PlayerController.Instance.animator.SetBool("InBattle", false);
    }
}
