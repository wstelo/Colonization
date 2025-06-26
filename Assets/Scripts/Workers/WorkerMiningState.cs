using System.Collections;
using UnityEngine;

public class WorkerMiningState : State
{
    private WorkerAnimatorController _controller;
    private WorkerBag _workerBags;
    private Worker _worker;
    private float _delayToMining = 0.9f;
    private float _damage;

    public WorkerMiningState(StateMachine stateMachine, WorkerAnimatorController animator, float damage, WorkerBag workerBags, Worker worker) : base(stateMachine)
    {
        _workerBags = workerBags;
        _controller = animator;
        _damage = damage;
        _worker = worker;
    }

    public override void Enter()
    {
        _controller.StartMiningAnimation();
        _worker.StartCoroutine(Mining());
    }

    private IEnumerator Mining()
    {
        var wait = new WaitForSeconds(_delayToMining);

        while (_worker.CurrentTargetResourse.CurrentHealth > 0)
        {
            yield return wait;

            _worker.CurrentTargetResourse.Extract(_damage);
        }

        _workerBags.PlaceProduct(_worker.CurrentTargetResourse);
        _worker.CurrentTargetResourse = null;
        StateMachine.SetState<WorkerReturnToBaseState>();
    }

    public override void Exit()
    {
        _controller.StopMiningAnimation();
    }
}
