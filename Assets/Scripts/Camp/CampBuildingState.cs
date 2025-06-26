using System;
using System.Collections.Generic;

public class CampBuildingState : State
{
    private CampWorkerHandler _workerHandler;
    private ResourseCollector _resourseCollector;
    private Camp _camp;

    private Dictionary<Type, int> _resoursesToNewBuilding = new Dictionary<Type, int>();
    private Dictionary<Type, int> _resoursesToNewWorker = new Dictionary<Type, int>();

    public CampBuildingState(StateMachine stateMachine, ResourseCollector collector, CampWorkerHandler workerHandler, Dictionary<Type, int> resoursesToNewBuilding, Dictionary<Type, int> resoursesToNewWorker, Camp camp) : base(stateMachine)
    {
        _resourseCollector = collector;
        _workerHandler = workerHandler;
        _resoursesToNewBuilding = resoursesToNewBuilding;
        _resoursesToNewWorker = resoursesToNewWorker;
        _camp = camp;
    }

    public override void Enter()
    {
        _resourseCollector.AmountChanged += TrySendWorkerToBuildConstruction;
    }

    public override void Exit()
    {
        _resourseCollector.AmountChanged -= TrySendWorkerToBuildConstruction;
    }

    private void TrySendWorkerToBuildConstruction()
    {
        if (_resourseCollector.TryGetResourseValue(_resoursesToNewBuilding) && _workerHandler.WorkerCount > 1)
        {
            _workerHandler.StartCoroutine(_workerHandler.SendWorkerToCampConstruction(_camp.CurrentBuildToConstruction, _resoursesToNewBuilding));
            _camp.ResetBuildToConstruction();
            StateMachine.SetState<CampIdleState>();
        }
        else if (_workerHandler.WorkerCount <= 1 && _resourseCollector.TryGetResourseValue(_resoursesToNewWorker))
        {
            _workerHandler.TryCreateNewWorker(_resoursesToNewWorker);
        }
    }
}
