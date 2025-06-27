using System;
using UnityEngine;

public class ResourseHealth : MonoBehaviour
{
    [SerializeField] private float _maxValue = 30f;

    public event Action Ended;
    public event Action<float> ValueChanged;

    public float CurrentValue { get; private set; }
    public float MaxValue => _maxValue;

    private void Awake()
    {
        Refresh();
    }

    public void TakeDamage(float damage)
    {
        if(damage > 0)
        {
            CurrentValue = Mathf.Clamp(CurrentValue - damage, 0, _maxValue);
            ValueChanged?.Invoke(CurrentValue);

            if (CurrentValue <= 0)
            {
                CurrentValue = 0;
                Ended?.Invoke();
            }          
        }
    }

    public void Refresh()
    {
        CurrentValue = _maxValue;
    }
}
