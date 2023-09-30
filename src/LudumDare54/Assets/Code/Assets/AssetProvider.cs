namespace LudumDare54
{
    public sealed class AssetProvider
    {
        public bool TryGet<T>(string path, out T asset) where T : UnityEngine.Object
        {
            asset = UnityEngine.Resources.Load<T>(path);
            return asset != null;
        }
    }
}