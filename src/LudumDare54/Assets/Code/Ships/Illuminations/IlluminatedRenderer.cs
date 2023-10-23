using Sirenix.OdinInspector;
using UnityEngine;

namespace LudumDare54
{
    public sealed class IlluminatedRenderer : MonoBehaviour
    {
        [Required] public Sprite[] ShipSprites;
        [Required] public SpriteRenderer ShipSpriteRenderer;

        public void SetSprite(float normalizedLightDirection)
        {
            int index = GetSpriteIndex(normalizedLightDirection, ShipSprites.Length);
            ShipSpriteRenderer.sprite = ShipSprites[index];
        }

        public static int GetSpriteIndex(float normalizedLightDirection, int spriteCount)
        {
            int index = Mathf.RoundToInt(normalizedLightDirection * spriteCount);
            index %= spriteCount;
            return index;
        }
    }
}