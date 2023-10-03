using System;
using System.Collections.Generic;
using Savidiy.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LudumDare54
{
    [CreateAssetMenu(fileName = nameof(LevelSettings), menuName = "Static Data/" + nameof(LevelSettings))]
    public sealed class LevelSettings : AutoSaveScriptableObject
    {
        public List<LevelIdField> LevelQueue = new();
    }

    [Serializable]
    public sealed class LevelIdField
    {
        [ValueDropdown(nameof(LevelIds)), HideLabel] public string LevelId;
        private ValueDropdownList<string> LevelIds => OdinLevelIdsProvider.LevelIds;
    }
}