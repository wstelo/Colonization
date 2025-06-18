using UnityEngine;
using TMPro;
using UnityEngine.UI;

internal class ResourceView : MonoBehaviour 
{
    [SerializeField] private Image _imageContainer;
    [SerializeField] private TMP_Text _countText;

    private Sprite _currentSprite;
    private int startCountValue = 0;

    public void Initialize(Sprite image)
    {
        _currentSprite = image;
        _imageContainer.sprite = _currentSprite;

        UpdateAmount(startCountValue);
    }

    public void UpdateAmount(int amount)
    {
        _countText.text = amount.ToString();
    }
}