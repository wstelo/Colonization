using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourseCreator : MonoBehaviour
{
    [SerializeField] List<ScriptableObjectResource> scriptableObjectResourses = new List<ScriptableObjectResource>();
    [SerializeField] private ResourceViewFactory _factory;
    [SerializeField] private Transform _gridLayoutGropComponent;

    private List<ResourceData> _resourses = new List<ResourceData>();
    private int _resourseChangeDelta = 1;

    public event Action <List<ResourceData>> ResourceCountChanged;

    private void Awake()
    {
        AddResourses();
    }

    private void AddResourses()
    {
        for (int i = 0; i < scriptableObjectResourses.Count; i++)
        {
            var resourse = new ResourceData(scriptableObjectResourses[i].Type, scriptableObjectResourses[i].Sprite);
            _factory.CreateViews(resourse, _gridLayoutGropComponent);
            _resourses.Add(resourse);
        }
    }

    public void AddAmount(Resourse resourse)
    {
        foreach(var item in _resourses)
        {
            if (item.Type == resourse.GetType())
            {
                item.AddAmount(_resourseChangeDelta);
            }
        }
    }
}
