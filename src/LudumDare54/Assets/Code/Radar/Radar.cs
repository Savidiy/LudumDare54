using System;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare54
{
    public sealed class Radar : IActivatable
    {
        private readonly HeroShipHolder _heroShipHolder;
        private readonly EnemiesHolder _enemiesHolder;
        private readonly IEventInvoker _eventInvoker;
        private readonly RadarSettings _radarSettings;
        private readonly float[] _radarProgress;
        private readonly bool[] _hasEnemyAtAngle;

        private IDisposable _subscription;

        public IReadOnlyList<float> RadarProgress => _radarProgress;

        public Radar(HeroShipHolder heroShipHolder, EnemiesHolder enemiesHolder, IEventInvoker eventInvoker,
            RadarSettings radarSettings)
        {
            _heroShipHolder = heroShipHolder;
            _enemiesHolder = enemiesHolder;
            _eventInvoker = eventInvoker;
            _radarSettings = radarSettings;
            _radarProgress = new float[_radarSettings.RadarLightData.Count];
            _hasEnemyAtAngle = new bool[_radarSettings.RadarLightData.Count];
        }

        public void Activate()
        {
            _subscription ??= _eventInvoker.Subscribe(UnityEventType.Update, OnUpdate);
            OnUpdate();
        }

        public void Deactivate()
        {
            _subscription?.Dispose();
            _subscription = null;
        }

        public void ClearProgress()
        {
            ClearEnemiesInfo(_hasEnemyAtAngle);
        }

        private void OnUpdate()
        {
            ClearEnemiesInfo(_hasEnemyAtAngle);
            UpdateEnemiesInfo(_hasEnemyAtAngle);
            UpdateRadarProgress(_hasEnemyAtAngle);
        }

        private static void ClearEnemiesInfo(bool[] hasEnemyAtAngle)
        {
            for (var i = 0; i < hasEnemyAtAngle.Length; i++)
                hasEnemyAtAngle[i] = false;
        }

        private void UpdateEnemiesInfo(bool[] hasEnemyAtAngle)
        {
            if (!_heroShipHolder.TryGetHeroShip(out Ship heroShip))
                return;

            Vector3 heroShipPosition = heroShip.Position;
            Quaternion heroShipRotation = heroShip.Rotation;
            float heroAngle = heroShipRotation.eulerAngles.z;
            for (var index = 0; index < _enemiesHolder.Ships.Count; index++)
            {
                Ship enemyShip = _enemiesHolder.Ships[index];

                Vector3 shipPosition = enemyShip.Position;
                Vector3 direction = shipPosition - heroShipPosition;
                float directionX = direction.x;
                float directionY = direction.y;
                float angle = Mathf.Atan2(-directionX, directionY) * Mathf.Rad2Deg;
                angle -= heroAngle;
                if (angle < -180)
                    angle += 360;
                else if (angle > 180)
                    angle -= 360;

                float angle2 = angle + 360;
                float angle3 = angle - 360;

                CheckAnglesData(hasEnemyAtAngle, angle, angle2, angle3);
            }
        }

        private void CheckAnglesData(bool[] hasEnemyAtAngle, float angle, float angle2, float angle3)
        {
            for (var i = 0; i < hasEnemyAtAngle.Length; i++)
            {
                if (hasEnemyAtAngle[i])
                    continue;

                RadarLightData radarLightData = _radarSettings.RadarLightData[i];
                float minAngle = radarLightData.Angles.x;
                float maxAngle = radarLightData.Angles.y;
                if (angle >= minAngle && angle <= maxAngle ||
                    angle2 >= minAngle && angle2 <= maxAngle ||
                    angle3 >= minAngle && angle3 <= maxAngle)
                {
                    hasEnemyAtAngle[i] = true;
                }
            }
        }

        private void UpdateRadarProgress(bool[] hasEnemyAtAngle)
        {
            float deltaTime = _eventInvoker.DeltaTime;
            float turnOnSpeed = deltaTime * _radarSettings.TurnOnSpeed;
            float turnOffSpeed = deltaTime * _radarSettings.TurnOffSpeed;

            for (var i = 0; i < hasEnemyAtAngle.Length; i++)
            {
                bool hasEnemy = hasEnemyAtAngle[i];
                float progress = _radarProgress[i];
                progress += hasEnemy ? turnOnSpeed : -turnOffSpeed;
                progress = Mathf.Clamp01(progress);

                _radarProgress[i] = progress;
            }
        }
    }
}