using UnityEngine;

public class SpawnPointHandler : MonoBehaviour
{
    [SerializeField] private TerrainLayerHandler _terrainLayerHandler;
    [SerializeField] private float _obstacleRadius = 5;
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private LayerMask _terrainMask;
    [SerializeField] private Terrain _terrain;

    private bool _isCorrectPoint = false;
    private bool _isCorrectHit = false;
    private float _maxHeight = 350f;
    private float _terrainHeight;
    private float _terrainWidth;

    private void Awake()
    {
        _terrainWidth = _terrain.terrainData.size.x;
        _terrainHeight = _terrain.terrainData.size.z;
    }

    public Vector3 GetPointOnNavMesh(Vector3 spawnPoint)
    {       
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(GetRayFromPoint(spawnPoint), out hit, Mathf.Infinity, _terrainMask);

        return hit.point;
    }

    public Vector3 GetPointsToSpawn(int layerIndex)
    {
        RaycastHit hit = new RaycastHit();
        _isCorrectHit = false;

        while (_isCorrectHit == false)
        {
            Vector3 spawnPoint = GetPointOnArea(layerIndex);
            Physics.Raycast(GetRayFromPoint(spawnPoint), out hit, Mathf.Infinity, _terrainMask | _obstacleMask);

            if (_terrainMask.IsContains(hit.collider.gameObject.layer))
            {
                Collider[] hits = new Collider[10];
                int hitsCount = Physics.OverlapSphereNonAlloc(hit.point, _obstacleRadius, hits, _obstacleMask);

                if (hitsCount == 0)
                {
                    _isCorrectHit = true;
                }
            }
        }

        return hit.point;
    }

    private Ray GetRayFromPoint(Vector3 point)
    {
        Vector3 spawnPoint = point;
        spawnPoint.y = _maxHeight;
        Ray ray = new Ray(spawnPoint, Vector3.down);

        return ray;
    }

    private Vector3 GetPointOnArea(int layerIndex)
    {
        Vector3 desiredPosition = new Vector3();
        _isCorrectPoint = false;

        while (_isCorrectPoint == false)
        {
            desiredPosition.x = Random.Range(_terrain.transform.position.x, _terrainWidth);
            desiredPosition.z = Random.Range(_terrain.transform.position.z, _terrainHeight);

            _isCorrectPoint = _terrainLayerHandler.IsCorrectPoint(desiredPosition, layerIndex);
        }

        return desiredPosition;
    }
}
