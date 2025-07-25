using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(EnemyShooting))]
[RequireComponent(typeof(EnemyAnimator))]
public class Enemy : MonoBehaviour
{
    private Health _health;
    private EnemyAnimator _animator;
    private EnemyShooting _shooting;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _animator = GetComponent<EnemyAnimator>();
        _shooting = GetComponent<EnemyShooting>();

        _health.OnDeath += HandleDeath;
    }

    private void OnEnable()
    {
        _shooting.enabled = true;
    }

    private void HandleDeath()
    {
        _shooting.enabled = false;

        StartCoroutine(_animator.PlayDeathAnimationAndDeactivate());
    }

    private void OnDestroy()
    {
        _health.OnDeath -= HandleDeath;
    }
}