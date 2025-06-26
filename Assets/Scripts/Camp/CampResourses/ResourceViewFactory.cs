using UnityEngine;

public class ResourceViewFactory : MonoBehaviour
{
    [SerializeField] private ResourceView _cellPrefab;

    public void CreateViews(ResourceData data, Transform parent)
    {
        var view = Instantiate(_cellPrefab, parent);
        view.Initialize(data);
    }
}
