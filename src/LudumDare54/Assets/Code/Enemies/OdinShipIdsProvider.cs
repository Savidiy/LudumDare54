using Savidiy.Utils;
using Sirenix.OdinInspector;

namespace LudumDare54
{
    public static class OdinShipIdsProvider
    {
        private static readonly EditorScriptableObjectLoader<ShipStaticDataLibrary> Loader = new();
        public static ValueDropdownList<string> ShipIds => Loader.GetAsset().ShipIds;
    }
}