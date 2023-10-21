using UnityEngine;

namespace LudumDare54
{
    public class HealthViewFactory
    {
        private const string ADDRESS = "Health";
        private readonly AssetProvider _assetProvider;

        public HealthViewFactory(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public HealthView Create(Transform root)
        {
            var prefab = _assetProvider.GetPrefab<HealthBehaviour>(ADDRESS);
            HealthBehaviour healthBehaviour = Object.Instantiate(prefab, root);
            return new HealthView(healthBehaviour);
        }
    }
}