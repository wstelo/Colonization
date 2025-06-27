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
        _inputHandler.SelectButtonPressed += InstallInitialCamp;
        _buildPreviewer.Activate(_buildConfig);
    }

    private void InstallInitialCamp()
    {
        if (_buildPreviewer.CurrentPreviewBuilding.HasObstacle == false)
        {
            Camp currentCamp = _campFactory.GetNewCamp(_buildConfig, _buildPreviewer.CurrentMousePosition);
            InitializeConstruction(currentCamp);
            currentCamp.CreateFirstWorker();
            _inputHandler.SelectButtonPressed -= InstallInitialCamp;
        }
    }

    private void CreateBuildPreview(Camp camp)
    {
        if (camp.CurrentBuildToConstruction != null)
        {
            _campFactory.DeleteBuildPreview(camp.CurrentBuildToConstruction);
        }

        _inputHandler.SelectButtonPressed += InstallBuildPreview;
        _inputHandler.CancelButtonPressed += CancelConstructionMode;
        _buildPreviewer.Activate(_buildConfig);
        _currentBuildPreview = _buildPreviewer.GetBuildPreview();
        _currentCamp = camp;
    }

    private void InstallBuildPreview()
    {
        if (_buildPreviewer.CurrentPreviewBuilding.HasObstacle == false)
        {
            _currentCamp.SetBuildingToConstruction(_currentBuildPreview);
            _currentBuildPreview.ConstructionEnded += FinishConstruction;
            _currentBuildPreview.DisableInstallationMode();
            _buildPreviewer.DisableConstructionMode();
            _inputHandler.SelectButtonPressed -= InstallBuildPreview;
            _inputHandler.CancelButtonPressed -= CancelConstructionMode;
            _currentBuildPreview = null;
            _currentCamp = null;
        }
    }

    private void FinishConstruction(BuildPreview build, Worker currentWorker)
    {
        Camp currentCamp = _campFactory.GetNewCamp(_buildConfig, build.transform.position);
        currentCamp.SetFirstWorker(currentWorker);
        InitializeConstruction(currentCamp);
        build.ConstructionEnded -= FinishConstruction;
    }

    private void InitializeConstruction(Camp currentCamp)
    {
        currentCamp.EnabledConstructionMode += CreateBuildPreview;
        currentCamp.DestroyedObject += UnsubscribeFromAction;
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
