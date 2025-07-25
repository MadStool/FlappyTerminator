using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public event Action OnDeath;
    public event Action<int, int> OnHealthChanged;

    [SerializeField] private int _maxHealth = 100;

    public int CurrentHealth { get; private set; }

    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth = Mathf.Max(0, CurrentHealth - damage);
        OnHealthChanged?.Invoke(CurrentHealth, _maxHealth);

        if (CurrentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
    }
}