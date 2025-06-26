using UnityEngine;
using TMPro;
using UnityEngine.UI;

internal class ResourceView : MonoBehaviour 
{
    [SerializeField] private Image _imageContainer;
    [SerializeField] private TMP_Text _countText;

    private Sprite _currentSprite;
    private int startCountValue = 0;
    private ResourceData _resourse;

    private void OnDestroy()
    {
        _resourse.AmountChanged -= UpdateAmount;
    }

    public void Initialize(ResourceData resourse)
    {
        _resourse = resourse;
        _currentSprite = resourse.Sprite;
        _imageContainer.sprite = _currentSprite;
        _resourse.AmountChanged += UpdateAmount;

        UpdateAmount(startCountValue);
    }

    public void UpdateAmount(int amount)
    {
        _countText.text = amount.ToString();
    }
}