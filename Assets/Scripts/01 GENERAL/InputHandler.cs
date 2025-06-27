using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";
    private const string ScrollWheel = "Mouse ScrollWheel";
    private const int LeftMouseButtonIndex = 0;
    private const int RightMouseButtonIndex = 1;
    private const KeyCode KeyCodePlus = KeyCode.KeypadPlus;
    private const KeyCode KeyCodeMinus = KeyCode.KeypadMinus;

    public event Action <float> OnValueChanged;
    public event Action SelectButtonPressed;
    public event Action CancelButtonPressed;
    public event Action KeyPadPlusButtonPressed;
    public event Action KeyPadMinusButtonPressed;

    public float HorizontalValue {  get; private set; }
    public float VerticalValue { get; private set; }
    public float ScrollWheelValue { get; private set; }

    private void Update()
    {
        HorizontalValue = Input.GetAxis(HorizontalAxis);
        VerticalValue = Input.GetAxis(VerticalAxis);

        if(Input.GetAxis(ScrollWheel) != 0)
        {
            ScrollWheelValue = Input.GetAxis(ScrollWheel);
            OnValueChanged?.Invoke(ScrollWheelValue);
        }
        
        if(Input.GetMouseButtonDown(LeftMouseButtonIndex))
        {
            SelectButtonPressed?.Invoke();
        }

        if(Input.GetKeyDown(KeyCodePlus))
        {
            KeyPadPlusButtonPressed?.Invoke();
        }

        if (Input.GetKeyDown(KeyCodeMinus))
        {
            KeyPadMinusButtonPressed?.Invoke();
        }

        if (Input.GetMouseButtonDown(RightMouseButtonIndex))
        {
            CancelButtonPressed?.Invoke();
        }
    }
}
