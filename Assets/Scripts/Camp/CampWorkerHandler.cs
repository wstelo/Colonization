using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CampWorkerHandler : MonoBehaviour
{
    private List<Worker> _unemployedWorkers = new List<Worker>();
    private ResourseHandler _resourseHandler;
    private WorkerCreator _workerCreator;
    private ResourseCollector _collector;
    private int _timeToRefreshResourse = 1;

    public int WorkerCount { get; private set; } = 0;

    private void Start()
    {
        StartCoroutine(SendWorkerToRandomResourse());
    }

    public void Initialize(ResourseHandler resourseHandler, WorkerCreator workerCreator, ResourseCollector resourseCollector)
    {
        _resourseHandler = resourseHandler;
        _workerCreator = workerCreator;
        _collector = resourseCollector;
    }

    public IEnumerator SendWorkerToCampConstruction(BuildPreview currentTarget, Dictionary<Type, int> resoursesToNewBuilding)
    {
        float timeToRefreshFreeWorker = 0.1f;
        var wait = new WaitForSeconds(timeToRefreshFreeWorker);
        bool hasWorkerToConstruction = false;

        while (hasWorkerToConstruction == false)
        {
            if (_unemployedWorkers.Count > 0)
            {
                Worker currentWorker = _unemployedWorkers[UnityEngine.Random.Range(0, _unemployedWorkers.Count)];
                _unemployedWorkers.Remove(currentWorker);
                currentWorker.SetConstructionCamp(currentTarget);
                _collector.RemoveAmount(resoursesToNewBuilding);
                hasWorkerToConstruction = true;
                WorkerCount -= 1;
            }
          
            yield return wait;
        }
    }

    public void TryCreateNewWorker(Dictionary<Type, int> resoursesToNewWorker)
    {
        int newWorkerCount = 1;

        if (_workerCreator.TryGetWorkers(newWorkerCount, out List<Worker> workers))
        {
            _unemployedWorkers.Add(workers.First());
            _collector.RemoveAmount(resoursesToNewWorker);
            WorkerCount += newWorkerCount;
        }       
    }

    public IEnumerator SendWorkerToRandomResourse()
    {
        var wait = new WaitForSeconds(_timeToRefreshResourse);

        while (enabled)
        {
            yield return wait;

            if (_resourseHandler.AvailableResourseCount != 0 && _unemployedWorkers.Count != 0)
            {
                Resourse nearestItem = _resourseHandler.GetRandomNearestResourse(transform.position);
                Worker currentWorker = _unemployedWorkers[UnityEngine.Random.Range(0, _unemployedWorkers.Count)];            
                currentWorker.SetCurrentResourse(nearestItem);
                currentWorker.FinishedWork += RefreshUnemployedWorkers;
                _unemployedWorkers.Remove(currentWorker);
            }       
        }
    }

    public void SetFirstWorker(Worker worker)
    {
        int firstWorkerCount = 1;
        _unemployedWorkers.Add(worker);
        WorkerCount += firstWorkerCount;
    }

    public void CreateFirstWorker()
    {
        int firstWorkerCount = 1;

        if (_workerCreator.TryGetWorkers(firstWorkerCount, out List<Worker> workers))
        {
            _unemployedWorkers = workers;
            WorkerCount += firstWorkerCount;
        }       
    }

    private void RefreshUnemployedWorkers(Worker worker)
    {
        _unemployedWorkers.Add(worker);
        worker.FinishedWork -= RefreshUnemployedWorkers;
    }
}
