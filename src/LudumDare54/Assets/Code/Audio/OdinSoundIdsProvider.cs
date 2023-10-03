using Savidiy.Utils;
using Sirenix.OdinInspector;

namespace LudumDare54
{
    public static class OdinSoundIdsProvider
    {
        private static readonly EditorScriptableObjectLoader<SoundLibrary> Loader = new();
        public static ValueDropdownList<string> SoundIds => Loader.GetAsset().SoundIds;
    }
}