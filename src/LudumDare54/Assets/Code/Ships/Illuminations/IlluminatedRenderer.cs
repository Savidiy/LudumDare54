using Sirenix.OdinInspector;
using UnityEngine;

namespace LudumDare54
{
    public sealed class IlluminatedRenderer : MonoBehaviour
    {
        [Required] public Sprite[] ShipSprites;
        [Required] public SpriteRenderer ShipSpriteRenderer;
        private static readonly int AShadowSpriteCoord = Shader.PropertyToID("_AShadowSpriteCoord");
        private static readonly int BShadowSpriteCoord = Shader.PropertyToID("_BShadowSpriteCoord");
        private static readonly int BaseSpriteCoord = Shader.PropertyToID("_BaseSpriteCoord");
        private static readonly int WeightA = Shader.PropertyToID("_WeightA");

        public void SetSprite(float normalizedLightDirection)
        {
            IlluminatePair illuminatePair = GetSpriteIndex(normalizedLightDirection, ShipSprites.Length);

            SetSpriteUV(BaseSpriteCoord, ShipSpriteRenderer.sprite);
            SetSpriteUV(AShadowSpriteCoord, ShipSprites[illuminatePair.SpriteIndexA]);
            SetSpriteUV(BShadowSpriteCoord, ShipSprites[illuminatePair.SpriteIndexB]);

            ShipSpriteRenderer.material.SetFloat(WeightA, illuminatePair.WeightA);
        }

        private void SetSpriteUV(int nameID, Sprite sprite)
        {
            Vector2 uvTransformA = CalcUVPosition(sprite);
            ShipSpriteRenderer.material.SetVector(nameID, uvTransformA);
        }

        private static Vector2 CalcUVPosition(Sprite sprite)
        {
            return new Vector2(
                sprite.rect.x / sprite.texture.width,
                sprite.rect.y / sprite.texture.height
            );
        }

        public static IlluminatePair GetSpriteIndex(float normalizedLightDirection, int spriteCount)
        {
            float floatIndex = normalizedLightDirection * spriteCount;
            int indexA = Mathf.RoundToInt(floatIndex);
            int indexB = Mathf.CeilToInt(floatIndex) == indexA ? indexA - 1 : indexA + 1;
            if (indexB < 0)
                indexB += spriteCount;

            float weightA = 1f - Mathf.Abs(floatIndex - indexA);
            indexA %= spriteCount;
            indexB %= spriteCount;
            return new IlluminatePair(indexA, weightA, indexB);
        }
    }
}