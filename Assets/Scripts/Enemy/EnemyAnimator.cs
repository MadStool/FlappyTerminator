using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        PlayFlyAnimation();
    }

    public void PlayFlyAnimation()
    {
        _animator.Play("FlyEnemy");
    }

    public IEnumerator PlayDeathAnimationAndDeactivate()
    {
        _animator.Play("DeathEnemy");

        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);

        gameObject.SetActive(false);
    }
}