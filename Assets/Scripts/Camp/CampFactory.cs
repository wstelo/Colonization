using System.Collections.Generic;
using UnityEngine;

public class CampFactory : MonoBehaviour
{
    [SerializeField] private ResourceViewFactory _resourseViewFactory;
    [SerializeField] private ResourseDataFactory _resourseDataFactory;
    [SerializeField] private ResourseHandler _resourseHandler;
    [SerializeField] private WorkerSpawner _workerSpawner;
    [SerializeField] private ResourseDataToBuilding _resourseDataToBuilding;
    [SerializeField] private SpawnPointHandler _spawnPointHandler;

    public Camp GetNewCamp(BuildConfig buildConfig, Vector3 position)
    {
        List<ResourceData> resourses = new List<ResourceData>();
        resourses = _resourseDataFactory.GetResourses();
        Camp currentCamp = Instantiate(buildConfig.BuildPrefab, position, Quaternion.identity);
        currentCamp.Initialize(_workerSpawner, _resourseHandler, resourses, _resourseDataToBuilding, _spawnPointHandler);

        foreach (ResourceData resourse in resourses)
        {
            _resourseViewFactory.CreateViews(resourse, currentCamp.ResourseViewPosition);
        }

        return currentCamp;
    }

    public BuildPreview GetNewCampPreview(BuildConfig buildConfig, Vector3 position)
    {
        BuildPreview build = Instantiate(buildConfig.BuildPreviewPrefab, position, Quaternion.identity);

        return build;
    }

    public void DeleteBuildPreview(BuildPreview buildPreview)
    {
        Destroy(buildPreview.gameObject);
    }
}
