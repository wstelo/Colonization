using UnityEngine;

public class CameraZoomer : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private InputHandler _inputHandler;

    [Header("ZoomValue")]
    [SerializeField] private float _minZoom = 60f;
    [SerializeField] private float _maxZoom = 110f;
    [SerializeField] private float _zoomSpeed = 20f;

    private Vector3 _nextPosition;

    private void OnEnable()
    {
        _inputHandler.OnValueChanged += ChangeValue;
    }

    private void OnDisable()
    {
        _inputHandler.OnValueChanged -= ChangeValue;
    }

    private void ChangeValue(float value)
    {
        _nextPosition = transform.position;
        _nextPosition.y = Mathf.Clamp(transform.position.y - value * _zoomSpeed, _minZoom, _maxZoom);

        transform.position = _nextPosition;
    }
}
