using System.Collections.Generic;
using UnityEngine;

public class ResourseDataToBuilding : MonoBehaviour
{
    [SerializeField] private BuildConfig _buildConfig;
    [SerializeField] private CharacterConfig _characterConfig;

    private Dictionary<ResourseType, int> _resoursesToCharacter = new Dictionary<ResourseType, int>();
    private Dictionary<ResourseType, int> _resoursesToConstructionBuilding = new Dictionary<ResourseType, int>();

    private void Awake()
    {
        _resoursesToCharacter = _characterConfig.GetRequiredResourses();
        _resoursesToConstructionBuilding = _buildConfig.GetRequiredResourses();
    }

    public Dictionary<ResourseType, int> GetResoursesToCharacter()
    {
        return _resoursesToCharacter;
    }

    public Dictionary<ResourseType, int> GetResoursesToCamp()
    {
        return _resoursesToConstructionBuilding;
    }

}
