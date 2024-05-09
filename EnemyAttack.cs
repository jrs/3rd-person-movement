using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int _damageAmount = 1;
    [SerializeField] private EnemyMovement _enemymovement;
    [SerializeField] private PlayerHealth _player;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("I hit the player");
            _enemymovement.EnemyAttack();
            _player.TakeDamage(_damageAmount);
        }
    }
}
