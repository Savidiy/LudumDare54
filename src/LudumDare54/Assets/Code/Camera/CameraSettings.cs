using Savidiy.Utils;
using UnityEngine;

namespace LudumDare54
{
    [CreateAssetMenu(fileName = nameof(CameraSettings), menuName = nameof(CameraSettings), order = 0)]
    public class CameraSettings : AutoSaveScriptableObject
    {
        public Vector3 HeroOffset;
    }
}