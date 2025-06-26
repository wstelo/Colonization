using System;
using UnityEngine;


public class TerrainLayersData : MonoBehaviour
{
    [SerializeField] private Terrain _terrain;

    [SerializeField] private TerrainLayer _snowLayer;
    [SerializeField] private TerrainLayer _planeLayer;
    [SerializeField] private TerrainLayer _desertLayer;

    private TerrainLayer[] _terrainLayers;

    public int SnowLayer { get; private set; } = int.MinValue;
    public int ForestLayer { get; private set; } = int.MinValue;
    public int DesertLayer { get; private set; } = int.MinValue;

    private void Awake()
    {
        _terrainLayers = _terrain.terrainData.terrainLayers;

        SnowLayer = GetIndex(_snowLayer);
        ForestLayer = GetIndex(_planeLayer);
        DesertLayer = GetIndex(_desertLayer);
    }
    
    private int GetIndex(TerrainLayer layer)
    {
        for (int i = 0; i < _terrainLayers.Length; i++)
        {
            if (_terrainLayers[i].name == layer.name)
            {
                return i;
            }
        }

        return int.MinValue;
    }
}
