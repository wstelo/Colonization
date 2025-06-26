using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorkerCreator : MonoBehaviour
{
    [SerializeField] private SpawnPointCollector _spawnPointCollector;

    private SpawnPointHandler _spawnPointHandler;
    private WorkerSpawner _workerSpawner;
    private List<Transform> _workerPoints;

    public int CurrentWorkerCount { get; private set; } = 0;
    public bool IsMaxWorkerValue { get; private set; } = false;

    private void Awake()
    {
        _workerPoints = _spawnPointCollector.TargetPoints;
    }

    public void SetPointToWorker(Worker worker)
    {
        Transform currentPositions = _workerPoints.First();
        worker.SetCurrentCamp(transform.position, _spawnPointHandler.GetPointOnNavMesh(currentPositions.position));
        _workerPoints.RemoveAt(0);
    }

    public bool TryGetWorkers(int count, out List<Worker> workers)
    {
        workers = new List<Worker>();

        if (count <= _workerPoints.Count)
        {
            for (int i = count - 1; i >= 0; i--)
            {
                Transform currentPositions = _workerPoints[i];
                Worker currentWorker = _workerSpawner.GetNewWorker(currentPositions);
                workers.Add(currentWorker);
                currentWorker.SetCurrentCamp(transform.position, _spawnPointHandler.GetPointOnNavMesh(currentPositions.position));
                _workerPoints.RemoveAt(i);
                CurrentWorkerCount++;
            }

            return true;
        }
        else
        {
            IsMaxWorkerValue = true;
        }

        return false;
    }

    public void Initialize(WorkerSpawner workerSpawner, SpawnPointHandler spawnPointHandler)
    {
        _spawnPointHandler = spawnPointHandler;
        _workerSpawner = workerSpawner;
    }
}
