using Unity.AI.Navigation;
using UnityEngine;

public class NavMeshBaker : MonoBehaviour
{
    [SerializeField] private NavMeshSurface _surface;
    [SerializeField] private BuildConstructor _constructionMode;

    private void OnEnable()
    {
        _constructionMode.BuildCreated += ReBakeSurface;
    }

    private void OnDisable()
    {
        _constructionMode.BuildCreated -= ReBakeSurface;
    }

    private void ReBakeSurface()
    {
        _surface.BuildNavMesh();
    }
}
