using Savidiy.Utils;
using UnityEngine;

namespace LudumDare54
{
    [CreateAssetMenu(fileName = nameof(CameraSettings), menuName = "Static Data/" + nameof(CameraSettings), order = 0)]
    public class CameraSettings : AutoSaveScriptableObject
    {
        public Vector3 HeroOffset;
    }
}