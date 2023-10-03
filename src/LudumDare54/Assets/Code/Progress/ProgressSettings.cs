using Savidiy.Utils;
using UnityEngine;

namespace LudumDare54
{
    [CreateAssetMenu(fileName = nameof(ProgressSettings), menuName = "Static Data/" + nameof(ProgressSettings))]
    public sealed class ProgressSettings : AutoSaveScriptableObject
    {
        public int StartLevel = 0;
        public bool TestMode;
        public int TestStartLevel = 0;
        public bool TestInvulnerability;
    }
}