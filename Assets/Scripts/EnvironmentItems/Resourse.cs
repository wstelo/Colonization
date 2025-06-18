using System;
using UnityEngine;

[RequireComponent(typeof(ResourseHealth))]

public abstract class Resourse : MonoBehaviour
{
    private ResourseHealth _health;

    public event Action<Resourse> Extracted;
    public event Action<Resourse> Collected;

    public float CurrentHealth => _health.CurrentValue;
    public float MaxHealth => _health.MaxValue;

    private void Awake()
    {
        _health = GetComponent<ResourseHealth>();
    }

    private void OnEnable()
    {
        _health.Refresh();
        _health.Ended += ExtractObject;
    }

    private void OnDisable()
    {   
        _health.Ended -= ExtractObject;
    }

    public abstract void Accept(IVisitor visitor);

    public void Extract(float damage)
    {
        _health.TakeDamage(damage);
    }

    private void ExtractObject()
    {
        Extracted?.Invoke(this);
    }

    public void CollectedObject()
    {
        Collected?.Invoke(this);
    }

    public void SetAsChild(Transform parentTransform)
    {
        transform.SetParent(parentTransform, true);
        transform.localPosition = Vector3.zero;
    }

    public void SetAsFree()
    {
        transform.SetParent(null, true);
    }
}
