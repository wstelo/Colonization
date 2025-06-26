using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourseDataToBuilding : MonoBehaviour
{
    [SerializeField] private BuildConfig _buildConfig;
    [SerializeField] private CharacterConfig _characterConfig;

    private Dictionary<Type, int> _resoursesToCharacter = new Dictionary<Type, int>();
    private Dictionary<Type, int> _resoursesToConstructionBuilding = new Dictionary<Type, int>();

    private void Awake()
    {
        _resoursesToCharacter.Add(ResourseType.Tree, _characterConfig.RequiredTreeCount);
        _resoursesToCharacter.Add(ResourseType.Stone, _characterConfig.RequiredStoneCount);
        _resoursesToCharacter.Add(ResourseType.Iron, _characterConfig.RequiredIronCount);
        _resoursesToConstructionBuilding.Add(ResourseType.Tree, _buildConfig.RequiredTreeCount);
        _resoursesToConstructionBuilding.Add(ResourseType.Iron, _buildConfig.RequiredIronCount);
        _resoursesToConstructionBuilding.Add(ResourseType.Stone, _buildConfig.RequiredStoneCount);
    }

    public Dictionary<Type,int> GetResoursesToCharacter()
    {
        return _resoursesToCharacter;
    }

    public Dictionary<Type, int> GetResoursesToCamp()
    {
        return _resoursesToConstructionBuilding;
    }

}
