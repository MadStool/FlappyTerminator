using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BulletAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.Play("PulseAnimation");
    }

    private void OnEnable()
    {
        if (_animator != null)
            _animator.Play("PulseAnimation");
    }
}