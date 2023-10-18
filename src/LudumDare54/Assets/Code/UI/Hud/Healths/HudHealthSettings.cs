using Savidiy.Utils;
using UnityEngine;

namespace LudumDare54
{
    [CreateAssetMenu(fileName = nameof(HudHealthSettings), menuName = "Static Data/" + nameof(HudHealthSettings), order = 0)]
    public sealed class HudHealthSettings : AutoSaveScriptableObject
    {
        public Sprite ActiveHearth;
        public Sprite InactiveHearth;
    }
}