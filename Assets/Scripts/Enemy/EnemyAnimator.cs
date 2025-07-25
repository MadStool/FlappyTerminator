using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private Animator _animator;
    private int _flyHash;
    private int _deathHash;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _flyHash = Animator.StringToHash("FlyEnemy");
        _deathHash = Animator.StringToHash("DeathEnemy");
    }

    public void PlayFlyAnimation()
    {
        _animator.Play(_flyHash);
    }

    public IEnumerator PlayDeathAnimationAndDeactivate()
    {
        _animator.Play(_deathHash);
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }
}