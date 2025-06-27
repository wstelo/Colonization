using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildPreview : MonoBehaviour
{
    [SerializeField] private Material _standartMaterial;
    [SerializeField] private Material _redMaterial;
    [SerializeField] private LayerMask _obstacleMask;

    private List<Collider> _currentObstacles = new List<Collider>();
    private MeshRenderer _currentMaterial;
    private bool _isActiveInstallationMode = true;

    public bool HasObstacle => _currentObstacles.Count > 0;

    public event Action<BuildPreview, Worker> ConstructionEnded;

    private void Awake()
    {
        _currentMaterial = GetComponent<MeshRenderer>();
        _currentMaterial.material = _standartMaterial;
    }

    public void EndedConstruction(Worker worker)
    {
        ConstructionEnded?.Invoke(this, worker);
    }

    public void DisableInstallationMode()
    {
        _isActiveInstallationMode = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isActiveInstallationMode)
        {
            if (_obstacleMask.IsContains(other.gameObject.layer))
            {
                _currentObstacles.Add(other);
            }

            if (_currentObstacles.Count > 0)
            {
                _currentMaterial.material = _redMaterial;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_isActiveInstallationMode)
        {
            if (_obstacleMask.IsContains(other.gameObject.layer))
            {
                _currentObstacles.Remove(other);
            }

            if (_currentObstacles.Count == 0)
            {
                _currentMaterial.material = _standartMaterial;
            }
        }
    }
}
