using FluentAssertions;
using LudumDare54;
using NUnit.Framework;

namespace Code.Tests.EditMode
{
    public sealed class IlluminatedRendererTests
    {
        [TestCase(0.000f, 1, 0, 1.00f, 0)]
        [TestCase(0.500f, 1, 0, 0.50f, 0)]
        [TestCase(1.000f, 1, 0, 1.00f, 0)]
        [TestCase(0.000f, 4, 0, 1.00f, 3)]
        [TestCase(0.062f, 4, 0, 0.75f, 1)]
        [TestCase(0.124f, 4, 0, 0.51f, 1)]
        [TestCase(0.126f, 4, 1, 0.51f, 0)]
        [TestCase(0.250f, 4, 1, 1.00f, 0)]
        [TestCase(0.374f, 4, 1, 0.51f, 2)]
        [TestCase(0.376f, 4, 2, 0.51f, 1)]
        [TestCase(0.500f, 4, 2, 1.00f, 1)]
        [TestCase(0.624f, 4, 2, 0.51f, 3)]
        [TestCase(0.626f, 4, 3, 0.51f, 2)]
        [TestCase(0.750f, 4, 3, 1.00f, 2)]
        [TestCase(0.874f, 4, 3, 0.51f, 0)]
        [TestCase(0.876f, 4, 0, 0.51f, 3)]
        [TestCase(1.000f, 4, 0, 1.00f, 3)]
        public void WhenCalcSpriteIndex(float normalizedLightDirection, int spriteCount, int indexA, float weightA, int indexB)
        {
            // act
            IlluminatePair illuminatePair = IlluminatedRenderer.GetSpriteIndex(normalizedLightDirection, spriteCount);

            // assert
            illuminatePair.SpriteIndexA.Should().Be(indexA);
            illuminatePair.SpriteIndexB.Should().Be(indexB);
            illuminatePair.WeightA.Should().BeApproximately(weightA, 0.01f);
        }
    }
}