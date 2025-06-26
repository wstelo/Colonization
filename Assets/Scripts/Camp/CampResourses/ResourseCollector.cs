using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourseCollector : MonoBehaviour
{
    private int _resourseChangeDelta = 1;
    private Dictionary<Type, ResourceData> _resourses = new Dictionary<Type, ResourceData>();

    public event Action AmountChanged;

    public void SetCurrentResourses(List<ResourceData> resourses)
    {
        foreach (ResourceData resourse in resourses)
        {
            _resourses.Add(resourse.Type, resourse);
        }
    }

    public bool TryGetResourseValue(Dictionary<Type, int> resourses)
    {
        foreach(var item in resourses)
        {
            if(_resourses.TryGetValue(item.Key, out ResourceData resourseData))
            {
                if(resourseData.Amount < item.Value)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void RemoveAmount(Dictionary<Type, int> resourses)
    {
        foreach (var item in resourses)
        {
            if(_resourses.TryGetValue(item.Key, out ResourceData resourseData))
            {
                resourseData.RemoveAmount(item.Value);
            }
        }

        AmountChanged?.Invoke();
    }

    public void AddAmount(Resourse resourse)
    {
        if(_resourses.TryGetValue(resourse.GetType(), out ResourceData resourseData))
        {
            resourseData.AddAmount(_resourseChangeDelta);
        }

        AmountChanged?.Invoke();
    }
}
