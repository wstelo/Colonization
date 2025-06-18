using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private float _smoothSpeed = 0.1f;

    private Vector3 _desiredPosition;

    private void FixedUpdate()
    {
        if(_inputHandler.HorizontalValue != 0 || _inputHandler.VerticalValue != 0)
        {
            _desiredPosition = transform.position + new Vector3(_inputHandler.HorizontalValue, 0, _inputHandler.VerticalValue);

            transform.position = Vector3.Lerp(transform.position, _desiredPosition, Time.fixedDeltaTime / _smoothSpeed);
        }
    }
}
