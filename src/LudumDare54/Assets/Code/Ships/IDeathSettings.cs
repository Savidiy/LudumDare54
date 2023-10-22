using UnityEngine;

namespace LudumDare54
{
    public interface IDeathSettings
    {
        bool HasDeathEffect { get; }
        EffectType EffectType { get; }
        Vector3 Position { get; }
    }
}