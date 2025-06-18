using UnityEngine;

public abstract class HealthBar : MonoBehaviour
{
    [SerializeField] protected ResourseHealth Health;

    private void OnEnable()
    {
        Health.ValueChanged += ChangeValue;
        Health.Ended += Disable;
    }

    private void OnDisable()
    {
        Health.ValueChanged -= ChangeValue;
        Health.Ended -= Disable;
    }

    public abstract void Disable();

    public abstract void ChangeValue(float value);
}
