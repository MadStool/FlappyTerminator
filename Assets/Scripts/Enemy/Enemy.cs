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
    private Collider2D _collider;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _animator = GetComponent<EnemyAnimator>();
        _shooting = GetComponent<EnemyShooting>();
        _collider = GetComponent<Collider2D>();

        _health.OnDeath += HandleDeath;
    }

    private void OnEnable()
    {
        _shooting.enabled = true;
        _collider.enabled = true;
    }

    private void HandleDeath()
    {
        _shooting.enabled = false;
        _collider.enabled = false;
        StartCoroutine(_animator.PlayDeathAnimationAndDeactivate());
    }

    private void OnDestroy()
    {
        _health.OnDeath -= HandleDeath;
    }
}