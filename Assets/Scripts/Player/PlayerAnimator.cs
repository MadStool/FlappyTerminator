using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        PlayFlyAnimation();
    }

    public void PlayFlyAnimation()
    {
        _animator.Play("FlyPlayer");
    }

    public IEnumerator PlayDeathAnimationAndDeactivate()
    {
        _animator.Play("DeathPlayer");

        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);

        gameObject.SetActive(false);
    }
}