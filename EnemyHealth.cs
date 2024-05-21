using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 5;
    private float _currentHealth;
    [SerializeField] Healthbar _healthbar;

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
        _healthbar.UpdateHealthbar(_maxHealth, _currentHealth);
    }

    void OnMouseDown()
    {
        _currentHealth -= Random.Range(0.5f, 1.5f);

        if (_currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            _healthbar.UpdateHealthbar(_maxHealth, _currentHealth);
        }
    }
}
