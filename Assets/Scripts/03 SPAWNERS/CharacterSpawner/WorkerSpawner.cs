using UnityEngine;

public class WorkerSpawner : MonoBehaviour
{
    [SerializeField] private SpawnPointHandler _spawnPointHandler;
    [SerializeField] private Worker _prefab;

    private Vector3 _position = new Vector3(0, 0, 0);

    public Worker GetNewWorker(Transform spawnPoint)
    {
        Vector3 newSpawnPoint = _spawnPointHandler.GetPointOnNavMesh(spawnPoint.position);
        Worker worker = Instantiate(_prefab, newSpawnPoint, Quaternion.identity, transform);

        return worker;
    }
}
