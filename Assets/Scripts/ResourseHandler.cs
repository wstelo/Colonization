using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static UnityEditor.Progress;

[RequireComponent(typeof(ResourseDetector))]

public class ResourseHandler : MonoBehaviour
{
    private List<Resourse> _resourses = new List<Resourse>();
    private ResourseDetector _resourseDetector;

    public int AvailableResourse => _resourses.Count;

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
        item.Extracted += RemoveExtractedResourse;
    }

    public void RemoveExtractedResourse(Resourse resourse)
    {
        _resourses.Remove(resourse);
        resourse.Extracted -= RemoveExtractedResourse;
    }

    public T GetNearestResourseByType<T>() where T : Resourse 
    {
        T resourse = _resourses.OfType<T>().OrderBy(x => gameObject.transform.position.SqrDistance(x.transform.position)).First();
        _resourses.Remove(resourse);
        
        return resourse;
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
