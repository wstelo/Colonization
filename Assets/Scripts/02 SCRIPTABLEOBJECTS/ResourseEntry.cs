using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourseEntry
{
    [SerializeField] private ResourseType _key;
    [SerializeField] private int _value;

    public ResourseType Key => _key;
    public int Value => _value;
}
