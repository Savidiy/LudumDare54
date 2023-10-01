using Savidiy.Utils;
using UnityEngine;

namespace LudumDare54
{
    [CreateAssetMenu(fileName = nameof(EnemiesSettings), menuName = "Static Data/" + nameof(EnemiesSettings),
        order = 0)]
    public sealed class EnemiesSettings : AutoSaveScriptableObject
    {
        public TurretSettings TurretSettings;
    }
}