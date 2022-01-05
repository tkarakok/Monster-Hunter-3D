using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : Singleton<PlayerCollisionController>
{
    [SerializeField] private GameObject _enemy;

    public GameObject Enemy { get => _enemy; set => _enemy = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            Enemy = other.gameObject;
        }
    }
}
