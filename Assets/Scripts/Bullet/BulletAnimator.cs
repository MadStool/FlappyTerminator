using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BulletAnimator : MonoBehaviour
{
    private Animator _animator;
    private int _pulseAnimationHash;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _pulseAnimationHash = Animator.StringToHash("PulseAnimation");
    }

    private void OnEnable()
    {
        _animator.Play(_pulseAnimationHash);
    }
}