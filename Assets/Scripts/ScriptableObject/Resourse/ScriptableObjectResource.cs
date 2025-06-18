using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewResourse", menuName = "Resourse / New Resourse")]

public class ScriptableObjectResource : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Resourse _resoursePrefab;
    
    public string Title => _resoursePrefab.GetType().ToString();
    public Sprite Sprite => _sprite;
    public Type Type => _resoursePrefab.GetType();
}