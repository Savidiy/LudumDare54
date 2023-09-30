using UnityEngine;

namespace LudumDare54
{
    public sealed class Bullet : IBullet
    {
        private readonly float _speed;
        private float _lifeTime;
        private readonly Transform _bulletTransform;

        public bool IsHeroBullet { get; }
        public bool IsAlive => _lifeTime > 0;

        public Vector3 Position => _bulletTransform.position;

        public Bullet(BulletBehaviour bulletBehaviour, float speed, float lifeTime, bool isHeroBullet)
        {
            _speed = speed;
            _lifeTime = lifeTime;
            IsHeroBullet = isHeroBullet;
            _bulletTransform = bulletBehaviour.transform;
        }

        public void UpdatePosition(float deltaTime)
        {
            _lifeTime -= deltaTime;

            float shift = deltaTime * _speed;
            _bulletTransform.position += _bulletTransform.up * shift;
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