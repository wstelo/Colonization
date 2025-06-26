using System;
using UnityEngine;

public class WorkerColllisionDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _resourseMask;

    public event Action <Resourse> ResourseDetected;
    public event Action <BuildPreview> BuildingDetected;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Resourse item))
        {
            ResourseDetected?.Invoke(item);
        }

        if(other.TryGetComponent(out BuildPreview build))
        {
            BuildingDetected?.Invoke(build);
        }
    }
}
