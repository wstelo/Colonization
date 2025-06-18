using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Build", menuName = "Build / New Build")]

public class BuildConfig : ScriptableObject
{
    [SerializeField] private BuildPreview _buildPreviewPrefab;
    [SerializeField] private Camp _buildPrefab;
    [SerializeField] private int _requiredTreeCount;
    [SerializeField] private int _requiredStoneCount;
    [SerializeField] private int _requiredIronCount;

    public BuildPreview BuildPreviewPrefab => _buildPreviewPrefab;
    public Camp BuildPrefab => _buildPrefab;
}
