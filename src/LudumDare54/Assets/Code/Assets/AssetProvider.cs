using UnityEngine;

namespace LudumDare54
{
    public class AssetProvider
    {
        public T GetPrefab<T>(string address) where T : MonoBehaviour
        {
            var monoBehaviour = Resources.Load<T>(address);
            return monoBehaviour;
        }
    }
}