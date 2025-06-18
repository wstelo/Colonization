using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPointCollector : MonoBehaviour
{
    [SerializeField] private List<Transform> _targetPoints;
    public List<Transform> TargetPoints => _targetPoints.ToList();

    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        int pointCount = transform.childCount;

        if (pointCount == 0)
        {
            throw new System.Exception("Точки отсутствуют.");
        }

        for (int i = 0; i < pointCount; i++)
            _targetPoints.Add(transform.GetChild(i));
    }
}
