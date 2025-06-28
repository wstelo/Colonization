using System.Collections.Generic;
using System;

public class ConstructionBehavior : IBaseBehavior
{
    private Dictionary<ResourseType, int> _resoursesToNewBuilding = new Dictionary<ResourseType, int>();
    private Dictionary<ResourseType, int> _resoursesToNewWorker = new Dictionary<ResourseType, int>();
    private ResourseCollector _resourseCollector;
    private CampWorkerHandler _workerHandler;
    private Camp _camp;

    public ConstructionBehavior(CampWorkerHandler workerHandler, ResourseCollector resourseCollector, Camp camp, Dictionary<ResourseType, int> resoursesToNewBuilding, Dictionary<ResourseType, int> resoursesToNewWorker)
    {
        _workerHandler = workerHandler;
        _resourseCollector = resourseCollector;
        _camp = camp;
        _resoursesToNewBuilding = resoursesToNewBuilding;
        _resoursesToNewWorker = resoursesToNewWorker;
    }

    public void StartBehavior()
    {
        _resourseCollector.AmountChanged += TrySendWorkerToBuildConstruction;
    }

    public void StopBehavior()
    {
        _resourseCollector.AmountChanged -= TrySendWorkerToBuildConstruction;
    }

    private void TrySendWorkerToBuildConstruction()
    {
        if (_resourseCollector.TryGetResourseValue(_resoursesToNewBuilding) && _workerHandler.WorkerCount > 1)
        {
            _workerHandler.StartCoroutine(_workerHandler.SendWorkerToCampConstruction(_camp.CurrentBuildToConstruction, _resoursesToNewBuilding));
            _camp.ClearBuildToConstruction();
        }
        else if (_workerHandler.WorkerCount <= 1 && _resourseCollector.TryGetResourseValue(_resoursesToNewWorker))
        {
            _workerHandler.TryCreateNewWorker(_resoursesToNewWorker);
        }
    }
}
