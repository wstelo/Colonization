using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WorkerCreator))]

public class Camp : MonoBehaviour
{
    [SerializeField] private ResourseHandler _resourseHandler;
    [SerializeField] private int _timeToRefreshResourse = 1;
    [SerializeField] private int _startWorkerCount = 3;

    private List<Worker> _unemployedWorkers = new List<Worker>();
    private WorkerCreator _workerCreator;

    private void Awake()
    {
        _workerCreator = GetComponent<WorkerCreator>();
    }

    private void Start()
    {
        StartCoroutine(SendWorkerToResourse());
        _unemployedWorkers = _workerCreator.GetWorkers(_startWorkerCount);
    }

    private IEnumerator SendWorkerToResourse()
    {
        var wait = new WaitForSeconds(_timeToRefreshResourse);

        while (enabled)
        {    
            if (_resourseHandler.AvailableResourse != 0 && _unemployedWorkers.Count > 0)
            {
                Resourse nearestItem = _resourseHandler.GetRandomNearestResourse(transform.position);
                Worker currentWorker = _unemployedWorkers[UnityEngine.Random.Range(0, _unemployedWorkers.Count)];
                _unemployedWorkers.Remove(currentWorker);
                currentWorker.SetCurrentResourse(nearestItem);
                currentWorker.FinishedWork += RefreshUnemployedWorkers;
            }

            yield return wait;
        }
    }

    private void RefreshUnemployedWorkers(Worker worker)
    {
        _unemployedWorkers.Add(worker);
        worker.FinishedWork -= RefreshUnemployedWorkers;
    }

    public void Initialize(WorkerSpawner workerSpawner, ResourseHandler resourseHandler)
    {
        _workerCreator.SetCurrentSpawner(workerSpawner);
        _resourseHandler = resourseHandler;
    }
}
