namespace LudumDare54
{
    public struct IlluminatePair
    {
        public readonly int SpriteIndexA;
        public readonly float WeightA;
        public readonly int SpriteIndexB;

        public IlluminatePair(int spriteIndexA, float weightA, int spriteIndexB)
        {
            SpriteIndexA = spriteIndexA;
            WeightA = weightA;
            SpriteIndexB = spriteIndexB;
        }
    }
}