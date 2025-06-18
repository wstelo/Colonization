using System;
using UnityEngine;

public class WorkerColllisionDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _resourseMask;

    public event Action <Resourse> ResourseDetected;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Resourse item))
        {
            ResourseDetected?.Invoke(item);
        }
    }
}
