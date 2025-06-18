using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildPreview : MonoBehaviour
{
    [SerializeField] private Material _standartMaterial;
    [SerializeField] private Material _redMaterial;
    [SerializeField] private LayerMask _obstacleMask;

    private List<Collider> _currentObstacles = new List<Collider>();
    private MeshRenderer _currentMaterial;

    public bool HasObstacle => _currentObstacles.Count > 0;

    private void Awake()
    {
        _currentMaterial = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        _currentMaterial.material = _standartMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {

        if(((1 << other.gameObject.layer) & _obstacleMask.value) != 0)
        {
            _currentObstacles.Add(other);
        }

        if(_currentObstacles.Count > 0)
        {
            _currentMaterial.material = _redMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & _obstacleMask.value) != 0)
        {
            _currentObstacles.Remove(other);
        }

        if (_currentObstacles.Count == 0)
        {
            _currentMaterial.material = _standartMaterial;
        }
    }
}
