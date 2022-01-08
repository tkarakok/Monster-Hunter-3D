using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitBarController : Singleton<HitBarController>
{
    public Image powerImage;
    private float _multiplier;
    private float _power = 0;
    private float _powerSpeed = 100;
    private bool _max = false;

    public float Multiplier { get => _multiplier; set => _multiplier = value; }

    private void Start()
    {
        ResetHitBar();
    }

    void Update()
    {
        if (StateManager.Instance.GameState == GameState.Battle && StateManager.Instance.BattleState == BattleState.PlayerTurn)
        {
            HitBar();
        }
    }
    void HitBar()
    {
        if (!_max)
        {
            _power += Time.deltaTime * _powerSpeed;
            if (_power >= 100)
            {
                _max = true;
                _power = 100;
            }
        }
        else
        {
            _power -= Time.deltaTime * _powerSpeed;
            if (_power <= 0)
            {
                _max = false;
                _power = 0;
            }
        }

        powerImage.fillAmount = _power / 100;
    }

    public void HitButtonFunction()
    {
        Multiplier = powerImage.fillAmount;
        PlayerController.Instance.InflictDamage();
        StateManager.Instance.BattleState = BattleState.EnemyTurn;
        EventManager.Instance.CheckBattleStateEvent(StateManager.Instance.BattleState);
    }

    public float CalculateMultiplier(float value)
    {
        if (value > 0 && value <= .4f)
        {
            value = .2f;
        }
        else if (value > .4f && value <= .8f)
        {
            value = .5f;
        }
        else
        {
            value = 1;
        }
        return value;
    }

    public void ResetHitBar()
    {
        _power = 0;
        _max = false;
        StateManager.Instance.BattleState = BattleState.PlayerTurn;
        UIManager.Instance.hitPanel.SetActive(true);
    }
}
