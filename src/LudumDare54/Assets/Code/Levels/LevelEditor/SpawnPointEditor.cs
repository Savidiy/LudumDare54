using Sirenix.OdinInspector;
using UnityEngine;

namespace LudumDare54
{
    internal sealed class SpawnPointEditor : MonoBehaviour
    {
        public ShipType ShipType;

        [Button]
        private void RandomAngle()
        {
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        } 
        
        [Button]
        private void RandomPosition()
        {
            transform.position = new Vector3(Random.Range(-4f, 4f), Random.Range(-4f, 4f), 0);
        }
        
        public void OnValidate()
        {
            name = $"Spawn - {ShipType.ToStringCached()}";
        }
    }
}