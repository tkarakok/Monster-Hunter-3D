using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : Singleton<AnimationManager>
{
    public void InGameAnimation()
    {
        //  transition ýn  game state
        PlayerController.Instance.animator.SetBool("Run",true);
    }

    public void StartBattleAnimation()
    {
        StartCoroutine(BattleAnimation());
    }
    // start player anim
    public void StartPlayerAttackAnimation()
    {
        StartCoroutine(PlayerAttackAnimation());
    }
    public void StartPlayerTakeDamageAnimation()
    {
        StartCoroutine(PlayerTakeDamageAnimation());
    }
    public void StartPlayerEscapeDamageAnimation()
    {
        StartCoroutine(PlayerTakeEscapeDamageAnimation());
    }

    IEnumerator BattleAnimation()
    {
        // transition battle state
        PlayerController.Instance.animator.SetBool("Run",false);
        PlayerController.Instance.animator.SetBool("Walk",true);
        yield return new WaitForSeconds(1);
        PlayerController.Instance.animator.SetBool("Walk", false);
    }
    // player attack and damage
    IEnumerator PlayerAttackAnimation()
    {
        // player attack anim
        PlayerController.Instance.animator.SetBool("Attack", true);
        yield return new WaitForSeconds(.26f);
        PlayerController.Instance.animator.SetBool("Attack", false);
    }
    IEnumerator PlayerTakeDamageAnimation()
    {
        PlayerController.Instance.animator.SetBool("TakeDamage", true);
        yield return new WaitForSeconds(.26f);
        PlayerController.Instance.animator.SetBool("TakeDamage", false);
    }
    IEnumerator PlayerTakeEscapeDamageAnimation()
    {
        PlayerController.Instance.animator.SetBool("Run",false);
        PlayerController.Instance.animator.SetBool("TakeDamage", true);
        yield return new WaitForSeconds(.26f);
        PlayerController.Instance.animator.SetBool("Run", true);
        PlayerController.Instance.animator.SetBool("TakeDamage", false);
    }

}
