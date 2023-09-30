using Savidiy.Utils;
using Sirenix.OdinInspector;

namespace LudumDare54
{
    public static class OdinShipStatIdsProvider
    {
        private static readonly EditorScriptableObjectLoader<ShipStatStaticDataLibrary> Loader = new();
        public static ValueDropdownList<string> ShipStatIds => Loader.GetAsset().ShipStatIds;
    }
}