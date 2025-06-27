using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaler : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;

    private int _timeDelta = 5;

    /// <summary>
    /// ///////////// TIMESCALE CLASS - ÑÎÇÄÀÍ ÈÑÊËŞ×ÈÒÅËÜÍÎ ÄËß ÒÎÃÎ, ×ÒÎ ÁÛ ÓÑÊÎĞÈÒÜ ÂÈÄÅÎ ÄËß ÑÄÀ×È ÄÇ è äëÿ òåñòîâ èãğû, ÷òî áû íå æäàòü äîëãî.
    /// </summary>

    private void OnEnable()
    {
        _inputHandler.KeyPadPlusButtonPressed += IncreaseValue;
        _inputHandler.KeyPadMinusButtonPressed += DecreaseValue;
    }

    private void OnDisable()
    {
        _inputHandler.KeyPadPlusButtonPressed -= IncreaseValue;
        _inputHandler.KeyPadMinusButtonPressed -= DecreaseValue;
    }

    private void IncreaseValue()
    {
        Time.timeScale += _timeDelta;       
    }

    private void DecreaseValue()
    {
        Time.timeScale = 1;
    }
}
