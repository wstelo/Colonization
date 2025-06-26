using System;
using UnityEngine;

public class ResourceData
{
    private int _amount;
    private int _initialAmount = 0;
    
    public event Action<int> AmountChanged;

    public ResourceData(Type type, Sprite image)
    {
        Sprite = image;
        Type = type;
        _amount = _initialAmount;
    }

    public Sprite Sprite { get; private set; }
    public Type Type { get; private set; }
    public int Amount
    {
        get => _amount;
        set
        {
            _amount = value;
            AmountChanged?.Invoke(_amount);
        }
    }

    public void AddAmount (int amount)
    {
        Amount += amount;
    }

    public void RemoveAmount (int amount)
    {
        Amount -= amount;
    }
}

