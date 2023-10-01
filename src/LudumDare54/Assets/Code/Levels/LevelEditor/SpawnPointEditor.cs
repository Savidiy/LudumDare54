using UnityEngine;

namespace LudumDare54
{
    internal sealed class SpawnPointEditor : MonoBehaviour
    {
        public ShipType ShipType;

        public void OnValidate()
        {
            name = $"Spawn - {ShipType.ToStringCached()}";
        }
    }
}