using Savidiy.Utils;
using Sirenix.OdinInspector;

namespace LudumDare54
{
    public static class OdinLevelIdsProvider
    {
        private static readonly EditorScriptableObjectLoader<LevelLibrary> Loader = new();
        public static ValueDropdownList<string> LevelIds => Loader.GetAsset().LevelIds;
    }
}