using Savidiy.Utils;
using UnityEngine;

namespace LudumDare54
{
    [CreateAssetMenu(fileName = nameof(InputSettings), menuName = "Static Data/" + nameof(InputSettings), order = 0)]
    public sealed class InputSettings : AutoSaveScriptableObject
    {
        public KeyCode[] Left;
        public KeyCode[] Right;
        public KeyCode[] Up;
        public KeyCode[] Down;
        public KeyCode[] StrafeLeft;
        public KeyCode[] StrafeRight;
        public KeyCode[] Fire1;
        public KeyCode[] Fire2;
        public KeyCode[] Fire3;
        public KeyCode[] Fire4;
        public KeyCode[] Menu;
    }
}