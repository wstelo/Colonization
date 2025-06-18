using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WorkerAnimatorController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartIdleAnimation()
    {
        _animator.SetBool(WorkerAnimationData.IsIdle, true);
    }

    public void StartWalkAnimation()
    {
        _animator.SetBool(WorkerAnimationData.IsWalk, true);
    }

    public void StartMiningAnimation()
    {
        _animator.SetBool(WorkerAnimationData.IsMining, true);
    }

    public void StopIdleAnimation()
    {
        _animator.SetBool(WorkerAnimationData.IsIdle, false);
    }

    public void StopWalkAnimation()
    {
        _animator.SetBool(WorkerAnimationData.IsWalk, false);
    }

    public void StopMiningAnimation()
    {
        _animator.SetBool(WorkerAnimationData.IsMining, false);
    }
}
