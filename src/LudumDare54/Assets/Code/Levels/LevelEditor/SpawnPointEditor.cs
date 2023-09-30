using Sirenix.OdinInspector;
using UnityEngine;

namespace LudumDare54
{
    internal sealed class SpawnPointEditor : MonoBehaviour
    {
        [ValueDropdown(nameof(EnemyIds))] public string EnemyId = "";
        private ValueDropdownList<string> EnemyIds => OdinShipIdsProvider.ShipIds;

        public void OnValidate()
        {
            name = $"Spawn - {EnemyId}";
        }
    }
}