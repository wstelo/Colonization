using System.Collections.Generic;
using UnityEngine;
using System;

public class WorkerCreateBehavior : IBaseBehavior
{
    private Dictionary<Type, int> _resoursesToNewWorker = new Dictionary<Type, int>();
    private CampWorkerHandler _workerHandler;
    private ResourseCollector _collector;
    private WorkerCreator _workerCreator;

    public WorkerCreateBehavior(ResourseCollector collector, CampWorkerHandler workerHandler, WorkerCreator workerCreator, Dictionary<Type, int> resoursesToNewWorker)
    {
        _resoursesToNewWorker = resoursesToNewWorker;
        _workerHandler = workerHandler;
        _workerCreator = workerCreator;
        _collector = collector;
    }

    public void StartBehavior()
    {
        _collector.AmountChanged += TryCreateNewWorker;
    }

    public void StopBehavior()
    {
        _collector.AmountChanged -= TryCreateNewWorker;
    }
    
    private void TryCreateNewWorker()
    {
        if (_collector.TryGetResourseValue(_resoursesToNewWorker) && _workerCreator.IsMaxWorkerValue == false)
        {
            _workerHandler.TryCreateNewWorker(_resoursesToNewWorker);
        }
    }
}
