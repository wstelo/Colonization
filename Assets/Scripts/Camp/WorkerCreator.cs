using System.Collections.Generic;
using UnityEngine;

public class WorkerCreator : MonoBehaviour
{
    [SerializeField] private SpawnPointCollector _spawnPointCollector;
    [SerializeField] private WorkerSpawner _workerSpawner;

    private List<Transform> _workerPoints;
    private int _currentWorkerCount = 0;

    private void Awake()
    {
        _workerPoints = _spawnPointCollector.TargetPoints;
    }

    public List<Worker> GetWorkers(int count)
    {
        if (_currentWorkerCount < _workerPoints.Count)
        {
            int workerToSpawn = Mathf.Min(count, _workerPoints.Count - _currentWorkerCount);
            List<Worker> workers = new List<Worker>();

            for (int i = workerToSpawn - 1; i >= 0; i--)
            {
                Transform currentPositions = _workerPoints[i];
                Worker currentWorker = _workerSpawner.GetNewWorker(currentPositions);
                workers.Add(currentWorker);
                currentWorker.SetCurrentCamp(transform.position, currentPositions.position);
                _workerPoints.RemoveAt(i);
            }

            return workers;
        }

        return null;
    }

    public void SetCurrentSpawner(WorkerSpawner workerSpawner)
    {
        _workerSpawner = workerSpawner;
    }
}
