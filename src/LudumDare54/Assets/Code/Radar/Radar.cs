using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LudumDare54
{
    public sealed class Radar : IActivatable
    {
        private readonly HeroShipHolder _heroShipHolder;
        private readonly EnemiesHolder _enemiesHolder;
        private readonly IEventInvoker _eventInvoker;
        private readonly RadarSettings _radarSettings;
        private readonly Transform _radarRoot;
        private readonly List<RadarPointBehaviour> _radarPoints = new();

        private IDisposable _subscription;

        public Radar(HeroShipHolder heroShipHolder, EnemiesHolder enemiesHolder, IEventInvoker eventInvoker,
            RadarSettings radarSettings)
        {
            _heroShipHolder = heroShipHolder;
            _enemiesHolder = enemiesHolder;
            _eventInvoker = eventInvoker;
            _radarSettings = radarSettings;
            _radarRoot = new GameObject("RadarRoot").transform;
        }

        public void Activate()
        {
            _subscription ??= _eventInvoker.Subscribe(UnityEventType.Update, OnUpdate);
        }

        public void Deactivate()
        {
            _subscription?.Dispose();
            _subscription = null;
            HideAll();
        }

        private void HideAll()
        {
            for (var index = 0; index < _radarPoints.Count; index++)
            {
                RadarPointBehaviour radarPointBehaviour = _radarPoints[index];
                radarPointBehaviour.gameObject.SetActive(false);
            }
        }

        private void OnUpdate()
        {
            HideAll();
            if (!_heroShipHolder.TryGetHeroShip(out Ship heroShip))
                return;

            Vector3 heroShipPosition = heroShip.Position;
            Quaternion heroShipRotation = heroShip.Rotation;
            for (var index = 0; index < _enemiesHolder.Ships.Count; index++)
            {
                Ship enemyShip = _enemiesHolder.Ships[index];

                Vector3 direction = enemyShip.Position - heroShipPosition;
                float distance = direction.magnitude;
                RadarPointBehaviour radarPointBehaviour = GetRadarPoint(index);
                radarPointBehaviour.gameObject.SetActive(true);
                Transform transform = radarPointBehaviour.transform;
                float shift = distance * _radarSettings.Scale + _radarSettings.MinOffset;
                transform.position = heroShipPosition + direction.normalized * shift;
                transform.rotation = heroShipRotation;
            }
        }

        private RadarPointBehaviour GetRadarPoint(int index)
        {
            if (index < _radarPoints.Count)
                return _radarPoints[index];

            RadarPointBehaviour radarPointBehaviour = Object.Instantiate(_radarSettings.RadarPointPrefab, _radarRoot);
            _radarPoints.Add(radarPointBehaviour);
            return radarPointBehaviour;
        }
    }
}