using UnityEngine;

namespace LudumDare54
{
    public sealed class HeroHealth : IShipHealth, ICanHasInvulnerability
    {
        private readonly HeroSettings _heroSettings;
        private readonly ProgressSettings _progressSettings;
        private readonly GameObject[] _blinkGameObjects;

        private int _health;
        private float _invulnerableTimer;
        private float _blinkTimer;

        public Vector3 LastAttackVector { get; private set; }
        public int Health => _health;
        public bool IsAlive => _health > 0;
        public bool IsDead => !IsAlive;
        public IShipDamage SelfDamageFromCollision { get; }
        public bool IsInvulnerable => _invulnerableTimer > 0;

        public HeroHealth(ShipBehaviour shipBehaviour, HeroSettings heroSettings, ProgressSettings progressSettings)
        {
            _heroSettings = heroSettings;
            _progressSettings = progressSettings;
            _health = heroSettings.StartHealth;
            _blinkGameObjects = shipBehaviour.BlinkGameObjects;
            SelfDamageFromCollision = new SimpleDamage(heroSettings.SelfDamageFromCollision);
            StartInvulnerable(_heroSettings.StartLevelInvulnerabilityTime);
        }

        public void TakeDamage(IShipDamage damage, Vector3 attackVector)
        {
            LastAttackVector = attackVector;
            int damageValue = damage.Damage;
#if UNITY_EDITOR
            if (_progressSettings.TestInvulnerability)
                damageValue = 0;
#endif
            _health -= damageValue;

            StartInvulnerable(_heroSettings.AfterDamageInvulnerabilityTime);
        }

        public void UpdateTimer(float deltaTime)
        {
            if (_invulnerableTimer <= 0)
                return;

            _invulnerableTimer -= deltaTime;
            UpdateBlink(deltaTime);
        }

        private void StartInvulnerable(float duration)
        {
            _invulnerableTimer = duration;
            _blinkTimer = _heroSettings.BlinkPeriod;
        }

        private void UpdateBlink(float deltaTime)
        {
            var isActive = true;
            if (_invulnerableTimer > 0)
            {
                _blinkTimer -= deltaTime;
                if (_blinkTimer < 0)
                    _blinkTimer = _heroSettings.BlinkPeriod;

                if (_blinkTimer > _heroSettings.BlinkPeriod / 2)
                    isActive = false;
            }

            foreach (GameObject gameObject in _blinkGameObjects)
                gameObject.SetActive(isActive);
        }
    }
}