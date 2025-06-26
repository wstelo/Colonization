using System;
using UnityEngine;
using UnityEngine.UI;

public class CampConstructionButton : MonoBehaviour
{
    [SerializeField] private Button _constructionModeButton;

    public event Action ButtonPressed;

    private void OnEnable()
    {
        _constructionModeButton.onClick.AddListener(PressButton);
    }

    private void OnDisable()
    {
        _constructionModeButton.onClick.RemoveListener(PressButton);
    }

    private void PressButton()
    {
        ButtonPressed?.Invoke();
    }
}
