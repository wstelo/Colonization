using System.Collections.Generic;
using System;

public class CampIdleState : State
{
    private Dictionary<Type, int> _resoursesToNewWorker = new Dictionary<Type, int>();
    private CampWorkerHandler _workerHandler;
    private ResourseCollector _collector;
    private WorkerCreator _workerCreator;
    private Camp _camp;

    public CampIdleState(StateMachine stateMachine, ResourseCollector collector, CampWorkerHandler workerHandler, WorkerCreator workerCreator, Dictionary<Type, int> resoursesToNewWorker, Camp camp) : base(stateMachine)
    {
        _collector = collector;
        _workerHandler = workerHandler;
        _resoursesToNewWorker = resoursesToNewWorker;
        _workerCreator = workerCreator;
        _camp = camp;
    }

    public override void Enter()
    {
        _collector.AmountChanged += TryCreateNewWorker;
        _camp.CampPreviewInstalled += ChangeStateToConstruction;
    }

    public override void Exit()
    {
        _collector.AmountChanged -= TryCreateNewWorker;
        _camp.CampPreviewInstalled -= ChangeStateToConstruction;
    }

    private void ChangeStateToConstruction()
    {
        StateMachine.SetState<CampBuildingState>();
    }

    private void TryCreateNewWorker()
    {
        if (_collector.TryGetResourseValue(_resoursesToNewWorker) && _workerCreator.IsMaxWorkerValue == false)
        {
            _workerHandler.TryCreateNewWorker(_resoursesToNewWorker);
        }
    }
}
