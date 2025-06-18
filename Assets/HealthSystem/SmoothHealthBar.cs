using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothHealthBar : HealthBar
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _delay = 1f;

    private Coroutine _coroutine;

    private void Start()
    {
        _slider.maxValue = Health.MaxValue;
        _slider.value = Health.CurrentValue;

        _slider.gameObject.SetActive(false);
    }

    public override void Disable()
    {
        _slider.gameObject.SetActive(false);
    }

    public override void ChangeValue(float value)
    {
        _slider.gameObject.SetActive(true);

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(SmoothChangeValue(value));
    }

    private IEnumerator SmoothChangeValue(float value)
    {
        while(Mathf.Approximately(value, _slider.value) == false)
        {
            yield return null;

            _slider.value = Mathf.MoveTowards(_slider.value, value, _delay * Time.deltaTime);
        }
    }
}
