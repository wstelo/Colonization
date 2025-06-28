using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New Build", menuName = "Build / New Build")]

public class BuildConfig : ScriptableObject
{
    [SerializeField] private BuildPreview _buildPreviewPrefab;
    [SerializeField] private Camp _prefab;
    [SerializeField] private List<ResourseEntry> _resourceList;

    public Dictionary<ResourseType, int> GetRequiredResourses()
    {
        return _resourceList.ToDictionary(x => x.Key, x => x.Value);
    }

    public BuildPreview BuildPreviewPrefab => _buildPreviewPrefab;
    public Camp BuildPrefab => _prefab;
}
