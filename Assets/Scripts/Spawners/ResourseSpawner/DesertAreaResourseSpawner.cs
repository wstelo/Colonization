
public class DesertAreaResourseSpawner : ResourseSpawner<Resourse>
{
    public override void Start()
    {
        LayerIndex = TerrainLayersData.DesertLayer;
        base.Start();
    }
}
