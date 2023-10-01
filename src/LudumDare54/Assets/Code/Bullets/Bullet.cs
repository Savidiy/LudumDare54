using UnityEngine;

namespace LudumDare54
{
    public sealed class Bullet : IBullet
    {
        private readonly float _speed;
        private readonly Transform _bulletTransform;
        private readonly BulletBehaviour _bulletBehaviour;

        private float _lifeTime;

        public IShipDamage Damage { get; }
        public bool IsHeroBullet { get; }
        public Collider2D Collider => _bulletBehaviour.Collider;
        public bool IsAlive => _lifeTime > 0;

        public Vector3 Position => _bulletTransform.position;

        public Bullet(BulletBehaviour bulletBehaviour, float speed, float lifeTime, bool isHeroBullet, IShipDamage damage)
        {
            _bulletBehaviour = bulletBehaviour;
            _speed = speed;
            _lifeTime = lifeTime;
            IsHeroBullet = isHeroBullet;
            Damage = damage;
            _bulletTransform = bulletBehaviour.transform;
        }

        public void UpdatePosition(float deltaTime)
        {
            float shift = deltaTime * _speed;
            _bulletTransform.position += _bulletTransform.up * shift;
        }

        public void UpdateLifetime(float deltaTime)
        {
            _lifeTime -= deltaTime;
        }

        public void ShiftPosition(Vector3 shift)
        {
            _bulletTransform.position += shift;
        }

        public void Dispose()
        {
            Object.Destroy(_bulletTransform.gameObject);
        }
    }
}