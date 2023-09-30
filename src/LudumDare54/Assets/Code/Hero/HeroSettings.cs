using Savidiy.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LudumDare54
{
    [CreateAssetMenu(fileName = nameof(HeroSettings), menuName = "Static Data/" + nameof(HeroSettings), order = 0)]
    public sealed class HeroSettings : AutoSaveScriptableObject
    {
        [ValueDropdown(nameof(ShipIds))] public string HeroShipStaticDataId;
        private ValueDropdownList<string> ShipIds => OdinShipIdsProvider.ShipIds;
    }
}