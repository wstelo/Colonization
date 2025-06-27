using System.Collections;
using UnityEngine;

public class BuildPreviewer : MonoBehaviour
{
    [SerializeField] private LayerMask _terrainMask;
    [SerializeField] private CampFactory _campFactory;

    private bool _isActiveConstructionMode = false;
    private Camera _camera;

    public BuildPreview CurrentPreviewBuilding { get; private set; }
    public Vector3 CurrentMousePosition { get; private set; } = Vector3.zero;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void Activate(BuildConfig buildConfig)
    {
        StartCoroutine(EnablePreviewMode(buildConfig));
    }

    public void DisableConstructionMode()
    {
        _isActiveConstructionMode = false;
    }

    public BuildPreview GetBuildPreview()
    {
        return CurrentPreviewBuilding;
    }

    public void DisableBuildPreviewer()
    {
        _isActiveConstructionMode = false;
        _campFactory.DeleteBuildPreview(CurrentPreviewBuilding);
    }

    private IEnumerator EnablePreviewMode(BuildConfig buildConfig)
    {
        CurrentPreviewBuilding = null;
        _isActiveConstructionMode = true;
        CurrentPreviewBuilding = _campFactory.GetNewCampPreview(buildConfig, CurrentMousePosition);

        while (_isActiveConstructionMode)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _terrainMask))
            {
                CurrentMousePosition = hit.point;
                CurrentPreviewBuilding.transform.position = CurrentMousePosition;
            }

            yield return null;
        }
    }
}
