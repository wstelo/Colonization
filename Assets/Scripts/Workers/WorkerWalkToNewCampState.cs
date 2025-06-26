using UnityEngine;

public class WorkerWalkToNewCampState : State
{
    private WorkerAnimatorController _animatorController;
    private WorkerColllisionDetector _collisionDetector;
    private Worker _worker;
    private Mover _mover;

    public WorkerWalkToNewCampState(StateMachine stateMachine, WorkerAnimatorController animator, Mover mover, Worker worker, WorkerColllisionDetector collisionDetector) : base(stateMachine)
    {
        _collisionDetector = collisionDetector;
        _animatorController = animator;
        _worker = worker;
        _mover = mover;
    }

    public override void Enter()
    {
        _animatorController.StartWalkAnimation();
        SetDesiredPosition(_worker.CurrentConstructionCamp.transform.position);
        _collisionDetector.BuildingDetected += ChangeStateToIdle;
    }

    public override void Exit()
    {
        _animatorController.StopWalkAnimation();
        _collisionDetector.BuildingDetected -= ChangeStateToIdle;
    }

    private void SetDesiredPosition(Vector3 target)
    {
        _mover.MoveToPoint(target);
    }

    private void ChangeStateToIdle(BuildPreview build)
    {
        if (build.transform.position == _worker.CurrentConstructionCamp.transform.position)
        {
            _worker.ClearCurrentBuilding();
            _mover.StopMovement();
            StateMachine.SetState<WorkerIdleState>();
        }
    }
}
