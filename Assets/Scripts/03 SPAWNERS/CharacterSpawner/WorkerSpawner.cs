using UnityEngine;

public class WorkerSpawner : CharacterSpawner<Worker>
{
    [SerializeField] private SpawnPointHandler _spawnPointHandler;

    public Worker GetNewWorker(Transform spawnPoint)
    {
        Vector3 newSpawnPoint = _spawnPointHandler.GetPointOnNavMesh(spawnPoint.position);
        var worker = Pool.Get();
        worker.gameObject.SetActive(false);
        worker.transform.position = newSpawnPoint;
        worker.gameObject.SetActive(true);

        return worker;
    }

    public override void ReleasedObject(Worker item)
    {
        base.ReleasedObject(item);
        Pool.Release(item);
    }

    public override void Initialize(Worker item)
    {
        base.Initialize(item);
        item.gameObject.SetActive(true);
    }

}
