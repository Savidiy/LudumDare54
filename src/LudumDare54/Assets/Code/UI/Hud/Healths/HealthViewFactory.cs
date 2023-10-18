using UnityEngine;

namespace LudumDare54
{
    public class HealthViewFactory
    {
        private const string ADDRESS = "Health";
        private readonly AssetProvider _assetProvider;
        private readonly HudHealthSettings _hudHealthSettings;

        public HealthViewFactory(AssetProvider assetProvider, HudHealthSettings hudHealthSettings)
        {
            _assetProvider = assetProvider;
            _hudHealthSettings = hudHealthSettings;
        }

        public HealthView Create(Transform root)
        {
            var prefab = _assetProvider.GetPrefab<HealthBehaviour>(ADDRESS);
            HealthBehaviour healthBehaviour = Object.Instantiate(prefab, root);
            return new HealthView(healthBehaviour, _hudHealthSettings);
        }
    }
}