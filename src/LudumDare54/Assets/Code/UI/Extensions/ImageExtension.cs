using UnityEngine;
using UnityEngine.UI;

namespace LudumDare54
{
    public static class ImageExtension
    {
        public static void SetAlpha(this Image image, float alpha)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }
}