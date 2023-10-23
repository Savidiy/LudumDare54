using FluentAssertions;
using LudumDare54;
using NUnit.Framework;

namespace Code.Tests.EditMode
{
    public sealed class IlluminatedRendererTests
    {
        [TestCase(0f, 1, 0)]
        [TestCase(0.5f, 1, 0)]
        [TestCase(1f, 1, 0)]
        [TestCase(0f, 4, 0)]
        [TestCase(0.05f, 4, 0)]
        [TestCase(0.124f, 4, 0)]
        [TestCase(0.126f, 4, 1)]
        [TestCase(0.374f, 4, 1)]
        [TestCase(0.376f, 4, 2)]
        [TestCase(0.624f, 4, 2)]
        [TestCase(0.626f, 4, 3)]
        [TestCase(0.874f, 4, 3)]
        [TestCase(0.876f, 4, 0)]
        [TestCase(1f, 4, 0)]
        public void WhenCalcSpriteIndex(float normalizedLightDirection, int spriteCount, int expectedIndex)
        {
            // act
            int foundedIndex = IlluminatedRenderer.GetSpriteIndex(normalizedLightDirection, spriteCount);

            // assert
            foundedIndex.Should().Be(expectedIndex);
        }
    }
}