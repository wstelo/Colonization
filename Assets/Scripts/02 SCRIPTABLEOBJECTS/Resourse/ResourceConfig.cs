using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewResourse", menuName = "Resourse / New Resourse")]

public class ResourceConfig : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Resourse _prefab;
    
    public Sprite Sprite => _sprite;
    public Type Type => _prefab.GetType();
}