using System;
using UnityEngine;

[RequireComponent(typeof(NavMeshBaker), typeof(BuildPreviewer))]

public class BuildConstructor : MonoBehaviour
{
    [SerializeField] private BuildPreviewer _buildPreviewer;
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private BuildConfig _buildConfig;
    [SerializeField] private CampFactory _campFactory;

    private BuildPreview _currentBuildPreview = null;
    private Camp _currentCamp = null;

    public event Action BuildCreated;

    private void Start()
    {
        CreateInitialCamp();
    }

    private void CreateInitialCamp()
    {
        _inputHandler.LeftMouseButtonPressed += InstallInitialCamp;
        StartCoroutine(_buildPreviewer.Activate(_buildConfig));
    }

    private void InstallInitialCamp()
    {
        if (_buildPreviewer.CurrentPreviewBuilding.HasObstacle == false)
        {
            Camp currentCamp = _campFactory.GetNewCamp(_buildConfig, _buildPreviewer.CurrentMousePosition);
            currentCamp.EnabledConstructionMode += CreateBuildPreview;
            currentCamp.DestroyedObject += UnsubscribeFromAction;
            currentCamp.CreateFirstWorker();
            _buildPreviewer.DisableBuildPreviewer();
            BuildCreated?.Invoke();
            _inputHandler.LeftMouseButtonPressed -= InstallInitialCamp;
        }
    }

    private void CreateBuildPreview(Camp camp)
    {
        if (camp.CurrentBuildToConstruction != null)
        {
            _campFactory.DeleteBuildPreview(camp.CurrentBuildToConstruction);
        }

        _inputHandler.LeftMouseButtonPressed += InstallBuildPreview;
        _inputHandler.RightMouseButtonPressed += CancelConstructionMode;
        StartCoroutine(_buildPreviewer.Activate(_buildConfig));
        _currentBuildPreview = _buildPreviewer.GetBuildPreview();
        _currentCamp = camp;
    }

    private void InstallBuildPreview()
    {
        if (_buildPreviewer.CurrentPreviewBuilding.HasObstacle == false)
        {
            _currentCamp.SetBuildingToConstruction(_currentBuildPreview);
            _currentBuildPreview.ConstructionEnded += FinishConstruction;
            _currentBuildPreview.DisabledInstallationMode();
            _buildPreviewer.DisableConstructionMode();
            _inputHandler.LeftMouseButtonPressed -= InstallBuildPreview;
            _inputHandler.RightMouseButtonPressed -= CancelConstructionMode;
            _currentBuildPreview = null;
            _currentCamp = null;
        }
    }

    private void FinishConstruction(BuildPreview build, Worker currentWorker)
    {
        Camp currentCamp = _campFactory.GetNewCamp(_buildConfig, build.transform.position);
        currentCamp.SetFirstWorker(currentWorker);
        currentCamp.EnabledConstructionMode += CreateBuildPreview;
        currentCamp.DestroyedObject += UnsubscribeFromAction;
        build.ConstructionEnded -= FinishConstruction;
        _buildPreviewer.DisableBuildPreviewer();
        BuildCreated?.Invoke();
    }

    private void UnsubscribeFromAction(Camp camp)
    {
        camp.EnabledConstructionMode -= CreateBuildPreview;
        camp.DestroyedObject -= UnsubscribeFromAction;
    }

    private void CancelConstructionMode()
    {
        _buildPreviewer.DisableBuildPreviewer();
    }
}
