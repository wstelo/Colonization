using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampConstructionHandler : MonoBehaviour
{
    [SerializeField] private CampConstructionButton _button;
    [SerializeField] private BuildConfig _campConfig;
    [SerializeField] private ResourseCreator _resourceCollector;

    private Camp _camp;

    private void Awake()
    {
        _camp = GetComponent<Camp>();
    }

    private void OnEnable()
    {
        _resourceCollector.ResourceCountChanged += RefreshConstructionStatus;
    }

    private void OnDisable()
    {
        
    }

    public void RefreshConstructionStatus(List<ResourceData> resourses)
    {
        
    }
}
