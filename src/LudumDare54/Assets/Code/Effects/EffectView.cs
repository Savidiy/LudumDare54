using UnityEngine;

namespace LudumDare54
{
    public class EffectView
    {
        private readonly EffectBehaviour _effectBehaviour;
        private readonly AnimationPlayer _animationPlayer;
        
        public EffectType EffectType { get; }
        public bool IsPlaying => _animationPlayer.IsPlaying;

        public EffectView(EffectBehaviour effectBehaviour, AnimationData animationData, EffectType effectType)
        {
            _effectBehaviour = effectBehaviour;
            EffectType = effectType;
            _animationPlayer = new AnimationPlayer(animationData, _effectBehaviour.SpriteRenderer);
        }

        public void Play()
        {
            _animationPlayer.Play();
        }

        public void Update(float deltaTime)
        {
            _animationPlayer.Update(deltaTime);
        }

        public void SetPosition(Vector3 position)
        {
            _effectBehaviour.transform.position = position;
        }

        public void SetRotation(Quaternion rotation)
        {
            _effectBehaviour.transform.rotation = rotation;
        }

        public void SetActive(bool isActive)
        {
            _effectBehaviour.gameObject.SetActive(isActive);
        }
    }
}