using System;

namespace LudumDare54
{
    public sealed class ShipHighlighter : IDisposable
    {
        private readonly SpriteHighlighter _spriteHighlighter;
        private readonly HighlightSettings _highlightSettings;

        public ShipHighlighter(SpriteHighlighter spriteHighlighter, HighlightSettings highlightSettings)
        {
            _spriteHighlighter = spriteHighlighter;
            _highlightSettings = highlightSettings;
        }

        public void Flash()
        {
            _spriteHighlighter.Flash(_highlightSettings);
        }

        public void Dispose()
        {
            _spriteHighlighter.Dispose();
        }
    }
}