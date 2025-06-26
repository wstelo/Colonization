using System;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(WorkerAnimatorController), typeof(WorkerColllisionDetector))]
[RequireComponent(typeof(WorkerBag))]

public class Worker : MonoBehaviour
{
    private WorkerAnimatorController _animatorController;
    private WorkerColllisionDetector _colllisionDetector;
    private StateMachine _stateMachine;
    private WorkerBag _workerBag;
    private Mover _mover;
    private float _workerDamage = 5f;

    public event Action <Worker> FinishedWork;
    public event Action AssignedNewTarget;

    public BuildPreview CurrentConstructionCamp { get; private set; } = null;
    public Resourse CurrentTargetResourse { get; set; } = null;
    public Resourse CurrentResourseInBag => _workerBag.CurrentProduct;
    public Vector3 CurrentPositionOnCamp { get; private set; } = Vector3.zero;
    public Vector3 CurrentCampPosition { get; private set; } = Vector3.zero;

    private void Awake()
    {
        _animatorController = GetComponent<WorkerAnimatorController>();
        _colllisionDetector = GetComponent<WorkerColllisionDetector>();
        _workerBag = GetComponent<WorkerBag>();
        _mover = GetComponent<Mover>();
    }

    private void OnEnable()
    {
        _colllisionDetector.BuildingDetected += EndedConstruction;
    }

    private void OnDisable()
    {
        _colllisionDetector.BuildingDetected -= EndedConstruction;
    }

    private void Start()
    {
        _stateMachine = new StateMachine();
        _stateMachine.AddState(new WorkerMiningState(_stateMachine, _animatorController, _workerDamage, _workerBag, this));
        _stateMachine.AddState(new WorkerWalkState(_stateMachine, _animatorController, _mover, _colllisionDetector, this));
        _stateMachine.AddState(new WorkerReturnToBaseState(_stateMachine, _animatorController, _mover, this));
        _stateMachine.AddState(new WorkerIdleState(_stateMachine, _animatorController, this));
        _stateMachine.AddState(new WorkerWalkToNewCampState(_stateMachine, _animatorController, _mover, this, _colllisionDetector));
        _stateMachine.SetState<WorkerIdleState>();
    }

    private void FixedUpdate()
    {
        _stateMachine?.FixedUpdate(); 
    }

    public void FinishWork()
    {
        CurrentTargetResourse = null;
        _workerBag.ResetCurrentResourse();
        FinishedWork?.Invoke(this);
    }

    public void SetConstructionCamp(BuildPreview target)
    {
        CurrentConstructionCamp = target;
        _stateMachine.SetState<WorkerWalkToNewCampState>();
    }

    public void SetCurrentCamp(Vector3 campPosition, Vector3 positionOnCamp)
    {
        CurrentPositionOnCamp = positionOnCamp;
        CurrentCampPosition = campPosition;
    }

    public void SetCurrentResourse(Resourse item)
    {
        CurrentTargetResourse = item;
        AssignedNewTarget?.Invoke();
    }

    public void ClearCurrentBuilding()
    {
        CurrentConstructionCamp = null;
    }

    private void EndedConstruction(BuildPreview build)
    {
        if(build == CurrentConstructionCamp)
        {
            build.EndedConstruction(this);
        }     
    }
}
