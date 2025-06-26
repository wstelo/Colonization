public class ForestAreaResourseSpawner : ResourseSpawner<Resourse>
{
    public override void Start()
    {
        LayerIndex = TerrainLayersData.ForestLayer;
        base.Start();
    }
}
