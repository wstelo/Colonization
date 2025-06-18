using UnityEngine;

public class WorkerReturnToBaseState : State
{
    private WorkerAnimatorController _controller;
    private Worker _worker;
    private Mover _mover;

    public WorkerReturnToBaseState(StateMachine stateMachine, WorkerAnimatorController animator, Mover mover, Worker worker) : base(stateMachine)
    {
        _controller = animator;
        _worker = worker;
        _mover = mover;
    }

    public override void Enter()
    {
        _controller.StartWalkAnimation();
        SetDesiredPosition(_worker.CurrentPositionOnCamp);
    }

    public override void FixedUpdate()
    {
        if (_mover.HasActivePath == false)
        {
            StateMachine.SetState<WorkerIdleState>();
        }
    }

    public override void Exit()
    {
        _controller.StopWalkAnimation();
    }

    private void SetDesiredPosition(Vector3 target)
    {
        _mover.MoveToPoint(target);
    }
}
