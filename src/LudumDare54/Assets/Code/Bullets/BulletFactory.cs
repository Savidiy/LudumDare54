using UnityEngine;

namespace LudumDare54
{
    public class BulletFactory
    {
        private readonly BulletLibrary _bulletLibrary;
        private readonly Transform _bulletRoot;

        public BulletFactory(BulletLibrary bulletLibrary)
        {
            _bulletLibrary = bulletLibrary;
            _bulletRoot = new GameObject("Bullets").transform;
        }

        public IBullet CreateBullet(BulletData bulletData, bool isHero)
        {
            Vector3 gunPosition = bulletData.StartPosition;
            Quaternion rotation = bulletData.Rotation;
            BulletStaticData bulletStaticData = _bulletLibrary.Get(bulletData.BulletId);
            BulletBehaviour prefab = bulletStaticData.BulletPrefab;
            BulletBehaviour bulletBehaviour = Object.Instantiate(prefab, gunPosition, rotation, _bulletRoot);

            float lifeTime = bulletStaticData.BulletLifeTime;
            float speed = bulletStaticData.BulletSpeed;
            var bullet = new Bullet(bulletBehaviour, speed, lifeTime, isHero, bulletData.Damage);
            return bullet;
        }
    }
}