using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(WorkerCreator), typeof(CampWorkerHandler))]

public class Camp : MonoBehaviour
{ 
    [SerializeField] private ResourseCollector _resourseCollector;
    [SerializeField] private Transform _resourseViewPosition;
    [SerializeField] private CampConstructionButton _constructionButton;

    private ResourseDataToBuilding _resourseData;
    private SpawnPointHandler _spawnPointHandler;
    private CampWorkerHandler _workerHandler;
    private ResourseHandler _resourseHandler;
    private WorkerCreator _workerCreator;
    private StateMachine _stateMachine;

    public event Action <Camp> EnabledConstructionMode;
    public event Action<Camp> DestroyedObject;
    public event Action CampPreviewInstalled;

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
        _resourseCollector.SetCurrentResourses(resourses);
        _resourseData = resourseBuildingData;
        _spawnPointHandler = spawnPointHandler;
        _workerCreator.Initialize(workerSpawner, _spawnPointHandler);
        _workerHandler.Initialize(_resourseHandler, _workerCreator, _resourseCollector);

        _stateMachine = new StateMachine();
        _stateMachine.AddState(new CampIdleState(_stateMachine, _resourseCollector, _workerHandler, _workerCreator , _resourseData.GetResoursesToCharacter(), this));
        _stateMachine.AddState(new CampBuildingState(_stateMachine, _resourseCollector, _workerHandler, _resourseData.GetResoursesToCamp(), _resourseData.GetResoursesToCharacter() ,this));
        _stateMachine.SetState<CampIdleState>();
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

    public void SetBuildingToConstruction( BuildPreview buildPreview)
    {
        CurrentBuildToConstruction = buildPreview;
        CampPreviewInstalled?.Invoke();
    }

    public void ResetBuildToConstruction()
    {
        CurrentBuildToConstruction = null;
    }

    private void EnableConstructionMode()
    {
        EnabledConstructionMode?.Invoke(this);
    }
}
