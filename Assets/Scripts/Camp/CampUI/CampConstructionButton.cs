using System;
using UnityEngine;
using UnityEngine.UI;

public class CampConstructionButton : MonoBehaviour
{
    [SerializeField] private Button _constructionModeButton;
    [SerializeField] private Sprite _activeButtonImage;
    [SerializeField] private Sprite _inactiveButtonImage;

    public event Action ButtonPressed;

    private void Start()
    {
        _constructionModeButton.image.sprite = _inactiveButtonImage;
    }

    private void OnEnable()
    {
        _constructionModeButton.onClick.AddListener(PressButton);
    }

    private void OnDisable()
    {
        _constructionModeButton.onClick.RemoveListener(PressButton);
    }

    private void SetActiveStatus()
    {
        _constructionModeButton.image.sprite = _activeButtonImage;
    }

    private void SetInactiveStatus()
    {
        _constructionModeButton.image.sprite = _inactiveButtonImage;
    }

    private void PressButton()
    {
        if(_constructionModeButton.image.sprite == _activeButtonImage)
        {
            ButtonPressed?.Invoke();
        }
    }
}
