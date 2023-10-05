using UnityEngine;
using UnityEngine.UI;

namespace LudumDare54
{
    public sealed class RadarLightBehaviour : MonoBehaviour
    {
        [SerializeField] private Image RadarOnImage;

        public void SetImageProgress(float progress)
        {
            Color color = RadarOnImage.color;
            color.a = progress;
            RadarOnImage.color = color;
        }
    }
}