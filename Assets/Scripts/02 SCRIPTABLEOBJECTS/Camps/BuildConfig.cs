using UnityEngine;

[CreateAssetMenu(fileName = "New Build", menuName = "Build / New Build")]

public class BuildConfig : ScriptableObject
{
    [SerializeField] private BuildPreview _buildPreviewPrefab;
    [SerializeField] private Camp _prefab;
    [SerializeField] private int _requiredTreeCount;
    [SerializeField] private int _requiredStoneCount;
    [SerializeField] private int _requiredIronCount;

    public int RequiredTreeCount => _requiredTreeCount;
    public int RequiredStoneCount => _requiredStoneCount;
    public int RequiredIronCount => _requiredIronCount;
    public BuildPreview BuildPreviewPrefab => _buildPreviewPrefab;
    public Camp BuildPrefab => _prefab;
}
