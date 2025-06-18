using UnityEngine;

public class ResourceViewFactory : MonoBehaviour
{
    [SerializeField] private ResourceView _cellPrefab;

    public void CreateViews(ResourceData item, Transform parent)
    {
        var view = Instantiate(_cellPrefab, parent);
        view.Initialize(item.Sprite);
        item.AmountChanged += view.UpdateAmount;
    }
}
