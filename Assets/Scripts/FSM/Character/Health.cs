using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int health;
    public event Action OnDie;

    public bool IsDead => health == 0;

    private void Start()
    {
        health = maxHealth; 
    }

    public void Init()
    {
        health = maxHealth; 
    }

    public void TakeDamage(int damage)
    {
        if (health == 0) return;
        health =  Mathf.Max(health - damage, 0);

        if (health == 0)
            OnDie?.Invoke();
    }
}
