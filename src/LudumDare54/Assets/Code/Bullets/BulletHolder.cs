using System.Collections.Generic;

namespace LudumDare54
{
    public sealed class BulletHolder
    {
        private readonly List<IBullet> _bullets = new();

        public IReadOnlyList<IBullet> Bullets => _bullets;

        public void AddBullet(IBullet bullet)
        {
            _bullets.Add(bullet);
        }

        public void RemoveAt(int index)
        {
            IBullet bullet = _bullets[index];
            bullet.Dispose();
            _bullets.RemoveAt(index);
        }

        public void Clear()
        {
            foreach (IBullet bullet in _bullets)
                bullet.Dispose();

            _bullets.Clear();
        }
    }
}