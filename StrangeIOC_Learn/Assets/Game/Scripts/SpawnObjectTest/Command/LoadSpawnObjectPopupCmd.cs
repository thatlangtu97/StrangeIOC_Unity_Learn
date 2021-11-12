using SpawnObject;

public class LoadSpawnObjectPopupCmd : AbsShowPopupCmd
{
    public override void Execute()
    {
        SpawnObjectView view = GetInstance<SpawnObjectView>();
    }

    protected override string GetAssetPath()
    {
        return AssetPaths.OBJECT_SPAWN;
    }
}
