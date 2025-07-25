using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(FlappyBird))]
[RequireComponent(typeof(PlayerShooting))]
[RequireComponent(typeof(PlayerAnimator))]
public class Player : MonoBehaviour
{
    private Health _health;
    private PlayerAnimator _animator;
    private FlappyBird _flappyBird;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _animator = GetComponent<PlayerAnimator>();
        _flappyBird = GetComponent<FlappyBird>();

        _health.OnDeath += HandleDeath;
    }

    private void HandleDeath()
    {
        _flappyBird.DisableControl();
        StartCoroutine(_animator.PlayDeathAnimationAndDeactivate());
    }

    private void OnDestroy()
    {
        _health.OnDeath -= HandleDeath;
    }
}