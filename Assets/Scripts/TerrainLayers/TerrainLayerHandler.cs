using UnityEngine;

[RequireComponent (typeof(TerrainLayersData))]
public class TerrainLayerHandler : MonoBehaviour
{
    [SerializeField] private Terrain _terrain;
    
    private float[,,] _alphaMap;
    private int _alphaMapHeight;
    private int _alphaMapWidth;
    private int _minPosX = 0;
    private int _maxPosY = 0;
    private float _minValue = 0.6f;

    private void Awake()
    {
        _alphaMapWidth = _terrain.terrainData.alphamapWidth;
        _alphaMapHeight = _terrain.terrainData.alphamapHeight;
        _alphaMap = _terrain.terrainData.GetAlphamaps(_minPosX, _maxPosY, _alphaMapWidth, _alphaMapHeight);
    }

    public bool IsCorrectPoint(Vector3 desiredPosition, int layerIndex)
    {
        Vector3 terrainPosition = desiredPosition - _terrain.transform.position;
        float xValue = terrainPosition.x / _terrain.terrainData.size.x;
        float zValue = terrainPosition.z / _terrain.terrainData.size.z;
        float xCoordinate = xValue * _alphaMapWidth;
        float zCoordinate = zValue * _alphaMapHeight;
        float value = _alphaMap[(int)zCoordinate, (int)xCoordinate, layerIndex];     
       
        if (value > _minValue)
        {
            return true;
        }

        return false;
    }
}
