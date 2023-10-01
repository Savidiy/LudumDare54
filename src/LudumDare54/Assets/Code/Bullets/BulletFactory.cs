using UnityEngine;

namespace LudumDare54
{
    public class BulletFactory
    {
        private readonly BulletSettings _bulletSettings;
        private readonly Transform _bulletRoot;

        public BulletFactory(BulletSettings bulletSettings)
        {
            _bulletSettings = bulletSettings;
            _bulletRoot = new GameObject("Bullets").transform;
        }

        public IBullet CreateBullet(BulletData bulletData, bool isHero)
        {
            Vector3 gunPosition = bulletData.GunPosition;
            Quaternion rotation = bulletData.Rotation;
            BulletBehaviour prefab = _bulletSettings.BulletPrefab;
            BulletBehaviour bulletBehaviour = Object.Instantiate(prefab, gunPosition, rotation, _bulletRoot);

            float lifeTime = _bulletSettings.BulletLifeTime;
            float speed = _bulletSettings.BulletSpeed;
            var bullet = new Bullet(bulletBehaviour, speed, lifeTime, isHero, bulletData.Damage);
            return bullet;
        }
    }
}