using Savidiy.Utils;
using UnityEngine;

namespace LudumDare54
{
    [CreateAssetMenu(fileName = nameof(BulletSettings), menuName = "Static Data/" + nameof(BulletSettings), order = 0)]
    public sealed class BulletSettings : AutoSaveScriptableObject
    {
        public BulletBehaviour BulletPrefab;
        [Min(0)] public float BulletSpeed;
        [Min(0)] public float BulletLifeTime;
    }
}