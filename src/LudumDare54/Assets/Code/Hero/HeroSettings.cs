using Savidiy.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LudumDare54
{
    [CreateAssetMenu(fileName = nameof(HeroSettings), menuName = "Static Data/" + nameof(HeroSettings), order = 0)]
    public sealed class HeroSettings : AutoSaveScriptableObject
    {
        public ShipBehaviour ShipBehaviourPrefab;

        [Title("Stats")]
        public float RotationSpeed;

        public float ForwardSpeed;
        public float BackwardSpeed;
        public float StrafeSpeed;
        public float ShootCooldown = 0.2f;
        public int ShootDamage = 1;
        public int StartHealth;
        public int SelfDamageFromCollision;

        [Title("Invulnerability")]
        [Min(0)] public float StartLevelInvulnerabilityTime = 3f;

        [Min(0)] public float AfterDamageInvulnerabilityTime = 1f;
        [Min(0)] public float BlinkPeriod = 0.2f;
    }
}