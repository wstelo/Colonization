using UnityEngine;

public class WorkerReturnToBaseState : State
{
    private WorkerAnimatorController _animatorController;
    private Worker _worker;
    private Mover _mover;
    private float _minDistanceToPoint = 1f;

    public WorkerReturnToBaseState(StateMachine stateMachine, WorkerAnimatorController animator, Mover mover, Worker worker) : base(stateMachine)
    {
        _animatorController = animator;
        _worker = worker;
        _mover = mover;
    }

    public override void Enter()
    {
        _animatorController.StartWalkAnimation();
        SetDesiredPosition(_worker.CurrentPositionOnCamp);
    }

    public override void FixedUpdate()
    {
        if(_worker.transform.position.IsEnoughClose(_worker.CurrentPositionOnCamp, _minDistanceToPoint))
        {
            StateMachine.SetState<WorkerIdleState>();
        }
    }

    public override void Exit()
    {
        _animatorController.StopWalkAnimation();
    }

    private void SetDesiredPosition(Vector3 target)
    {
        _mover.MoveToPoint(target);
    }
}
