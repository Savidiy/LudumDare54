using Savidiy.Utils;
using UnityEngine;

namespace LudumDare54
{
    [CreateAssetMenu(fileName = nameof(ProgressSettings), menuName = "Static Data/" + nameof(ProgressSettings),
        order = 0)]
    public sealed class ProgressSettings : AutoSaveScriptableObject
    {
        public int StartLevel = 0;
        public bool SkipMenuInEditor;
        public int TestStartLevel = 0;
    }
}