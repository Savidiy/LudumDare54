using UnityEngine;

namespace LudumDare54
{
    public sealed class LimitedSpaceChecker
    {
        private readonly HeroShipHolder _heroShipHolder;
        private readonly EnemiesHolder _enemiesHolder;
        private readonly LevelDataProvider _levelDataProvider;

        public LimitedSpaceChecker(HeroShipHolder heroShipHolder, EnemiesHolder enemiesHolder, LevelDataProvider levelDataProvider)
        {
            _heroShipHolder = heroShipHolder;
            _enemiesHolder = enemiesHolder;
            _levelDataProvider = levelDataProvider;
        }
        
        public void Check()
        {
            if (!_heroShipHolder.TryGetHeroShip(out Ship heroShip))
                return;

            Vector3 anchorPosition = heroShip.Position;
            LevelStaticData levelStaticData = _levelDataProvider.GetCurrentLevel();
            float width = levelStaticData.Width;
            float height = levelStaticData.Height;
            for (var index = 0; index < _enemiesHolder.Ships.Count; index++)
            {
                Ship ship = _enemiesHolder.Ships[index];
                CorrectPosition(ship, anchorPosition, width, height);
            }
        }

        private void CorrectPosition(Ship ship, Vector3 anchorPosition, float width, float height)
        {
            Vector3 position = ship.Position;
            Vector3 direction = position - anchorPosition;
            float halfWidth = width / 2f;
            float halfHeight = height / 2f;
            Vector3 shift = Vector3.zero;
            if (direction.x > halfWidth)
                shift.x -= width;
            else if (direction.x < -halfWidth)
                shift.x += width;
            
            if (direction.y > halfHeight)
                shift.y -= height;
            else if (direction.y < -halfHeight)
                shift.y += height;

            ship.ShiftPosition(shift);
        }
    }
}