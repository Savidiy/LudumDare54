using UnityEngine;

namespace LudumDare54
{
    public class AnimationPlayer
    {
        private readonly AnimationData _animationData;
        private readonly SpriteRenderer _spriteRenderer;
        private float _timer;
        private int _frameIndex;

        public bool IsPlaying { get; private set; }

        public AnimationPlayer(AnimationData animationData, SpriteRenderer spriteRenderer)
        {
            _animationData = animationData;
            _spriteRenderer = spriteRenderer;
        }

        public void Play()
        {
            if (_animationData.Frames.Count == 0)
                return;

            IsPlaying = true;
            _timer = 0;
            _frameIndex = 0;

            _spriteRenderer.sprite = _animationData.Frames[0].Sprite;
        }

        public void Update(float deltaTime)
        {
            if (!IsPlaying)
                return;

            float delta = deltaTime * _animationData.Speed;
            _timer += delta;

            FrameData frameData = _animationData.Frames[_frameIndex];
            float frameDuration = frameData.UseCustomDuration ? frameData.CustomDuration : _animationData.DefaultFrameDuration;

            if (_timer < frameDuration)
                return;

            _timer -= frameDuration;
            _frameIndex++;
            if (_frameIndex >= _animationData.Frames.Count)
            {
                _frameIndex = 0;
                IsPlaying = false;
            }
            else
            {
                _spriteRenderer.sprite = _animationData.Frames[_frameIndex].Sprite;
            }
        } 
    }
}