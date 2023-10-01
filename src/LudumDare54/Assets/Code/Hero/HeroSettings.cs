using Savidiy.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace LudumDare54
{
    [CreateAssetMenu(fileName = nameof(HeroSettings), menuName = "Static Data/" + nameof(HeroSettings), order = 0)]
    public sealed class HeroSettings : AutoSaveScriptableObject
    {
        [FormerlySerializedAs("HeroShipStaticDataId")] [ValueDropdown(nameof(ShipIds))] public string HeroShipId;
        private ValueDropdownList<string> ShipIds => OdinShipIdsProvider.ShipIds;

        [Min(0)] public float StartLevelInvulnerabilityTime = 3f;
        [Min(0)] public float AfterDamageInvulnerabilityTime = 1f;
        [Min(0)] public float BlinkPeriod = 0.2f;
    }
}