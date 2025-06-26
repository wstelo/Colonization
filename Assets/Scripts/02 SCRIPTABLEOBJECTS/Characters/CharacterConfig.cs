using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character / NewCharacter")]

public class CharacterConfig : ScriptableObject
{
    [SerializeField] private Worker _prefab;
    [SerializeField] private int _requiredTreeCount;
    [SerializeField] private int _requiredStoneCount;
    [SerializeField] private int _requiredIronCount;

    public int RequiredTreeCount => _requiredTreeCount;
    public int RequiredStoneCount => _requiredStoneCount;
    public int RequiredIronCount => _requiredIronCount;
    public Worker BuildPrefab => _prefab;
}
