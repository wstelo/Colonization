using System.Collections.Generic;
using UnityEngine;

public class ResourseDataFactory : MonoBehaviour
{
    [SerializeField] List<ResourceConfig> _resourseItems = new List<ResourceConfig>();

    public List<ResourceData> GetResourses()
    {
        List<ResourceData> _resourses = new List<ResourceData>();

        for (int i = 0; i < _resourseItems.Count; i++)
        {
            var resourse = new ResourceData(_resourseItems[i].Type, _resourseItems[i].Sprite);
            _resourses.Add(resourse);
        }

        return _resourses;
    }
}
