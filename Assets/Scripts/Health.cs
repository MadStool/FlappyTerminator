using UnityEngine;
using System;
using static Unity.VisualScripting.Member;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    private IDamageHandler[] _damageHandlers;

    public event Action<int, int> OnHealthChanged;

    public int CurrentHealth { get; private set; }
    public int MaxHealth { get; private set; }

    private void Awake()
    {
        MaxHealth = _maxHealth;
        CurrentHealth = MaxHealth;
        _damageHandlers = GetComponents<IDamageHandler>();
    }

    public void TakeDamage(int damage, Transform damageSource)
    {
        Debug.Log($"Taking {damage} damage from {damageSource.name}");

        CurrentHealth = Mathf.Max(0, CurrentHealth - damage);
        Debug.Log($"New health: {CurrentHealth}");

        foreach (IDamageHandler handler in _damageHandlers)
            handler.HandleDamage(damage, damageSource);

        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);

        if (CurrentHealth <= 0)
            Die();

        Debug.Log($"{name} taking {damage} damage from {damageSource.name}");
    }

    public void Heal(int amount)
    {
        CurrentHealth = Mathf.Min(MaxHealth, CurrentHealth + amount);

        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    private void Die()
    {
        foreach (IDamageHandler handler in _damageHandlers)
            handler.HandleDeath();

        FlappyBird player = GetComponent<FlappyBird>();

        if (player != null)
        {
            player.DisableControl();
        }

        EnemyAnimator enemyAnimator = GetComponent<EnemyAnimator>();
        PlayerAnimator playerAnimator = GetComponent<PlayerAnimator>();

        if (enemyAnimator != null)
        {
            StartCoroutine(enemyAnimator.PlayDeathAnimationAndDeactivate());
        }
        else if (playerAnimator != null)
        {
            StartCoroutine(playerAnimator.PlayDeathAnimationAndDeactivate());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}