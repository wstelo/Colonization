using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(ResourseDetector))]

public class ResourseHandler : MonoBehaviour
{
    private List<Resourse> _resourses = new List<Resourse>();
    private ResourseDetector _resourseDetector;

    public int AvailableResourseCount => _resourses.Count;

    private void Awake()
    {
        _resourseDetector = GetComponent<ResourseDetector>();
    }

    private void OnEnable()
    {
        _resourseDetector.Entered += AddObject;
    }

    private void OnDisable()
    {
        _resourseDetector.Entered -= AddObject;
    }

    private void AddObject(Resourse item)
    {
        _resourses.Add(item);  
    }

    public Resourse GetRandomNearestResourse(Vector3 nearestPosition)
    {
        if(_resourses.Count != 0)
        {
            Resourse resourse = _resourses.OrderBy(x => nearestPosition.SqrDistance(x.transform.position)).First();
            _resourses.Remove(resourse);

            return resourse;
        }

        return null;
    }
}
