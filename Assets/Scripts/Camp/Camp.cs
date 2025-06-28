using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WorkerCreator), typeof(CampWorkerHandler))]

public class Camp : MonoBehaviour
{
    [SerializeField] private ResourseCollector ResourseCollector;
    [SerializeField] private Transform _resourseViewPosition;
    [SerializeField] private CampConstructionButton _constructionButton;

    private ResourseDataToBuilding _resourseData;
    private SpawnPointHandler _spawnPointHandler;
    private CampWorkerHandler _workerHandler;
    private ResourseHandler _resourseHandler;
    private WorkerCreator _workerCreator;
    private IBaseBehavior _baseBehavior;
    private ConstructionBehavior _constructionBehavior;
    private WorkerCreateBehavior _workerCreateBehavior;

    public event Action<Camp> EnabledConstructionMode;
    public event Action<Camp> DestroyedObject;

    
    public BuildPreview CurrentBuildToConstruction { get; private set; } = null;
    public Transform ResourseViewPosition => _resourseViewPosition;

    private void Awake()
    {
        _constructionButton.ButtonPressed += EnableConstructionMode;
        _workerCreator = GetComponent<WorkerCreator>();
        _workerHandler = GetComponent<CampWorkerHandler>();
    }

    private void OnDestroy()
    {
        _constructionButton.ButtonPressed -= EnableConstructionMode;
        DestroyedObject?.Invoke(this);
    }

    public void Initialize(WorkerSpawner workerSpawner, ResourseHandler resourseHandler, List<ResourceData> resourses, ResourseDataToBuilding resourseBuildingData, SpawnPointHandler spawnPointHandler)
    {
        _resourseHandler = resourseHandler;
        ResourseCollector.SetCurrentResourses(resourses);
        _resourseData = resourseBuildingData;
        _spawnPointHandler = spawnPointHandler;
        _workerCreator.Initialize(workerSpawner, _spawnPointHandler);
        _workerHandler.Initialize(_resourseHandler, _workerCreator, ResourseCollector);

        _constructionBehavior = new ConstructionBehavior(_workerHandler, ResourseCollector, this, _resourseData.GetResoursesToCamp(), _resourseData.GetResoursesToCharacter());
        _workerCreateBehavior = new WorkerCreateBehavior(ResourseCollector, _workerHandler, _workerCreator, _resourseData.GetResoursesToCharacter());
        SetBehavior(_workerCreateBehavior);
    }

    public void SetFirstWorker(Worker worker)
    {
        _workerHandler.SetFirstWorker(worker);
        _workerCreator.SetPointToWorker(worker);
    }

    public void CreateFirstWorker()
    {
        _workerHandler.CreateFirstWorker();
    }

    public void SetBuildingToConstruction(BuildPreview buildPreview)
    {
        CurrentBuildToConstruction = buildPreview;
        SetBehavior(_constructionBehavior);
    }

    public void ClearBuildToConstruction()
    {
        CurrentBuildToConstruction = null;
        SetBehavior(_workerCreateBehavior);
    }

    private void EnableConstructionMode()
    {
        EnabledConstructionMode?.Invoke(this);
    }

    private void SetBehavior<T>(T behavior) where T : IBaseBehavior
    {
        var type = typeof(T);

        if (_baseBehavior?.GetType() == type)
        {
            return;
        }

        _baseBehavior?.StopBehavior();
        _baseBehavior = behavior;
        _baseBehavior.StartBehavior();
    }
}
