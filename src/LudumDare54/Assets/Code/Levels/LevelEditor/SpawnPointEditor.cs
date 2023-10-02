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

        public float RandomDistance = 4f;
        
        [Button]
        private void RandomPosition()
        {
            float half = RandomDistance/2;
            transform.position = new Vector3(Random.Range(-half, half), Random.Range(-half, half), 0);
        }
        
        public void OnValidate()
        {
            name = $"Spawn - {ShipType.ToStringCached()}";
        }
    }
}