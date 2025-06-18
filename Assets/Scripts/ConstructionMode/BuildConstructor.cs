using System;
using System.Collections;
using UnityEngine;

public class BuildConstructor : MonoBehaviour
{
    [SerializeField] private ResourseHandler _resourseHandler;
    [SerializeField] private WorkerSpawner _workerSpawner;
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private BuildConfig _buildConfig;
    [SerializeField] private LayerMask _terrainMask;

    private BuildPreview _currentPreviewBuilding;
    private Camera _camera;
    private Vector3 _currentMousePosition = Vector3.zero;
    private bool _isActiveConstructionMode = false;

    public event Action BuildCreated;

    private void Awake()
    {
        _camera = Camera.main;
        _currentPreviewBuilding = Instantiate(_buildConfig.BuildPreviewPrefab, _currentMousePosition, Quaternion.identity);
        _currentPreviewBuilding.gameObject.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(SetBuildingPoint());
    }

    private IEnumerator SetBuildingPoint()
    {
        _isActiveConstructionMode = true;
        _inputHandler.LeftMouseButtonPressed += InstallNewCamp;
        _currentPreviewBuilding.gameObject.SetActive(true);

        while (_isActiveConstructionMode)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _terrainMask))
            {
                _currentMousePosition = hit.point;
                _currentPreviewBuilding.transform.position = _currentMousePosition;
            }

            yield return null;
        }

        _inputHandler.LeftMouseButtonPressed -= InstallNewCamp;
    }

    private void InstallNewCamp()
    {
        Destroy(_currentPreviewBuilding.gameObject);
        Camp currentCamp = Instantiate(_buildConfig.BuildPrefab, _currentMousePosition, Quaternion.identity);
        currentCamp.Initialize(_workerSpawner, _resourseHandler);
        BuildCreated?.Invoke();
        _isActiveConstructionMode = false;
        _currentPreviewBuilding.gameObject.SetActive(false);
    }
}
