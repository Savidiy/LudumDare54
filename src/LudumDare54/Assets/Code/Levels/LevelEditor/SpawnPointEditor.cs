using Sirenix.OdinInspector;
using UnityEngine;

namespace LudumDare54
{
    internal sealed class SpawnPointEditor : MonoBehaviour
    {
        public ShipType ShipType;
#if UNITY_EDITOR
        [Button]
        private void RandomAngle()
        {
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        }

        [Button]
        private void RandomPosition()
        {
            var levelEditor = GetComponentInParent<LevelEditor>();
            if (levelEditor == null)
            {
                Debug.LogError("No LevelEditor found");
                return;
            }
            
            float half = levelEditor.LevelWidth / 2f;
            transform.position = new Vector3(Random.Range(-half, half), Random.Range(-half, half), 0);
        }
        
        public void OnValidate()
        {
            name = $"Spawn - {ShipType.ToStringCached()}";
        }
#endif
    }
}