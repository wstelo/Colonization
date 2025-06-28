using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character / NewCharacter")]

public class CharacterConfig : ScriptableObject
{
    [SerializeField] private Worker _prefab;
    [SerializeField] private List<ResourseEntry> _resourceList;

    public Dictionary<ResourseType, int> GetRequiredResourses()
    {
        return _resourceList.ToDictionary(x => x.Key, x => x.Value);
    }

    public Worker BuildPrefab => _prefab;
}
