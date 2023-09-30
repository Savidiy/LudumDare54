using System;
using System.Collections.Generic;
using Savidiy.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LudumDare54
{
    [CreateAssetMenu(fileName = nameof(LevelLibrary), menuName = "Static Data/" + nameof(LevelLibrary), order = 0)]
    public sealed class LevelLibrary : AutoSaveScriptableObject
    {
        [SerializeField, ListDrawerSettings(ListElementLabelName = "@this")]
        private List<LevelStaticData> LevelStaticData = new();

        public ValueDropdownList<string> LevelIds { get; } = new();

        public bool TryGetLevelStaticData(string levelId, out LevelStaticData levelStaticData)
        {
            foreach (LevelStaticData staticData in LevelStaticData)
            {
                if (string.Equals(staticData.LevelId, levelId, StringComparison.InvariantCulture))
                {
                    levelStaticData = staticData;
                    return true;
                }
            }

            levelStaticData = null;
            return false;
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            LevelIds.Clear();
            foreach (LevelStaticData levelStaticData in LevelStaticData)
                LevelIds.Add(levelStaticData.LevelId);
        }

        public void CreateLevel(string newLevelId)
        {
            LevelStaticData.Add(new LevelStaticData {LevelId = newLevelId});
        }
    }

    [Serializable]
    public sealed class LevelStaticData
    {
        public string LevelId = "";
        public List<SpawnPointStaticData> SpawnPoints = new();

        public override string ToString()
        {
            return $"{LevelId}({SpawnPoints.Count})";
        }
    }

    [Serializable]
    public sealed class SpawnPointStaticData
    {
        public Vector3 Position;
        public Vector3 Rotation;
        [ValueDropdown(nameof(EnemyIds))] public string EnemyId = "";
        private ValueDropdownList<string> EnemyIds => OdinShipIdsProvider.ShipIds;
    }
}