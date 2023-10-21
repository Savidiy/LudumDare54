using UnityEngine;

namespace LudumDare54
{
    public class HealthView
    {
        private readonly HealthBehaviour _behaviour;
        private readonly Vector3 _sizeRootStartLocalScale;

        private float _animationTime;

        public bool IsActive { get; private set; }

        public HealthView(HealthBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        public void InstantActivate()
        {
            _behaviour.PlayableDirector.Stop();
            _behaviour.OnImage.SetActive(true);
            _behaviour.OffImage.SetActive(false);
            _behaviour.WhiteImage.SetActive(false);
            _behaviour.gameObject.SetActive(true);
            IsActive = true;
        }

        public void ShowLostHealth()
        {
            _behaviour.PlayableDirector.Play();
            IsActive = false;
        }

        public void Hide()
        {
            _behaviour.PlayableDirector.Stop();
            _behaviour.gameObject.SetActive(false);
            IsActive = false;
        }
    }
}