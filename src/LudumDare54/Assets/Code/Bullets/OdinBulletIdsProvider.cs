using Savidiy.Utils;
using Sirenix.OdinInspector;

namespace LudumDare54
{
    public static class OdinBulletIdsProvider
    {
        private static readonly EditorScriptableObjectLoader<BulletLibrary> Loader = new();
        public static ValueDropdownList<string> BulletIds => Loader.GetAsset().BulletIds;
    }
}