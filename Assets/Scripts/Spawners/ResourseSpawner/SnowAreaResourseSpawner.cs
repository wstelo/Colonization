
public class SnowAreaResourseSpawner : ResourseSpawner<Resourse>
{
    public override void Start()
    {
        LayerIndex = TerrainLayersData.SnowLayer;
        base.Start();
    }
}
