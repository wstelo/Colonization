using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewResourse", menuName = "Resourse / New Resourse")]

public class ResourceConfig : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Resourse _prefab;
    [SerializeField] private ResourseType _resourceType;
    
    public Sprite Sprite => _sprite;
    public ResourseType Type => _resourceType;
}