using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace LudumDare54
{
    public sealed class StarField : IActivatable
    {
        private readonly IEventInvoker _eventInvoker;
        private readonly LimitedSpaceChecker _limitedSpaceChecker;
        private readonly HeroShipHolder _heroShipHolder;
        private readonly StarFieldSettings _starFieldSettings;
        private readonly LevelDataProvider _levelDataProvider;
        private readonly Transform _starRoot;
        private readonly List<StarBehaviour> _stars = new();

        private float _starBlinkTimer;
        private int _blinkIndex;
        private Vector3 _previousHeroShipPosition;
        private IDisposable _updateSubscribe;

        public StarField(IEventInvoker eventInvoker, LimitedSpaceChecker limitedSpaceChecker,
            HeroShipHolder heroShipHolder, StarFieldSettings starFieldSettings, LevelDataProvider levelDataProvider)
        {
            _eventInvoker = eventInvoker;
            _limitedSpaceChecker = limitedSpaceChecker;
            _heroShipHolder = heroShipHolder;
            _starFieldSettings = starFieldSettings;
            _levelDataProvider = levelDataProvider;
            _starRoot = new GameObject("StarField").transform;
            _stars.Capacity = _starFieldSettings.StarCount;
        }

        public void CreateStars()
        {
            LevelStaticData levelStaticData = _levelDataProvider.GetCurrentLevel();
            float width = levelStaticData.Width;
            float height = levelStaticData.Height;
            float halfHeight = height / 2;
            float halfWidth = width / 2;

            for (int i = 0; i < _starFieldSettings.StarCount; i++)
            {
                var starPosition = new Vector3(Random.Range(-halfWidth, halfWidth), Random.Range(-halfHeight, halfHeight), 0f);
                StarBehaviour starPrefab = GetRandom(_starFieldSettings.StarPrefabs);
                StarBehaviour star = Object.Instantiate(starPrefab, starPosition, Quaternion.identity, _starRoot);
                Color[] colors = GetRandom(_starFieldSettings.StarColors);
                float minStarMoveMultiplier = _starFieldSettings.MinStarMoveMultiplier;
                float maxStarMoveMultiplier = _starFieldSettings.MaxStarMoveMultiplier;
                float moveMultiplier = Random.Range(minStarMoveMultiplier, maxStarMoveMultiplier);
                star.Initialize(colors, moveMultiplier);
                _stars.Add(star);
            }
        }

        public void ClearStars()
        {
            foreach (StarBehaviour starBehaviour in _stars)
                Object.Destroy(starBehaviour.gameObject);

            _stars.Clear();
        }

        public void Activate()
        {
            _updateSubscribe ??= _eventInvoker.Subscribe(UnityEventType.Update, OnUpdate);
        }

        public void Deactivate()
        {
            _updateSubscribe?.Dispose();
            _updateSubscribe = null;
        }

        private T GetRandom<T>(List<T> list) => list[Random.Range(0, list.Count)];

        private void OnUpdate()
        {
            BlinkStars();
            MoveStars();
        }

        private void BlinkStars()
        {
            if (_starFieldSettings.StarBlinkPeriod <= 0)
                return;

            _starBlinkTimer += Time.deltaTime;

            while (_starBlinkTimer > _starFieldSettings.StarBlinkPeriod)
            {
                _starBlinkTimer -= _starFieldSettings.StarBlinkPeriod;
                StarBehaviour starBehaviour = _stars[_blinkIndex];
                starBehaviour.SetRandomColor();

                _blinkIndex++;
                if (_blinkIndex >= _stars.Count)
                {
                    _blinkIndex = 0;
                    Shuffle(_stars);
                }
            }
        }

        private void MoveStars()
        {
            if (!_heroShipHolder.TryGetHeroShip(out Ship ship))
                return;

            Vector3 shipPosition = ship.Position;
            Quaternion shipRotation = ship.Rotation;
            Vector3 deltaPosition = shipPosition - _previousHeroShipPosition;

            for (var index = 0; index < _stars.Count; index++)
            {
                StarBehaviour starBehaviour = _stars[index];
                Transform transform = starBehaviour.transform;
                transform.rotation = shipRotation;
                transform.position += deltaPosition * starBehaviour.MoveMultiplier;
            }

            _previousHeroShipPosition = shipPosition;
            CheckLimitedSpace(shipPosition);
        }

        private void CheckLimitedSpace(Vector3 shipPosition)
        {
            if (Time.frameCount % _starFieldSettings.StarMoveFrameRate != 0)
                return;

            LevelStaticData levelStaticData = _levelDataProvider.GetCurrentLevel();
            float width = levelStaticData.Width;
            float height = levelStaticData.Height;

            for (var index = 0; index < _stars.Count; index++)
            {
                StarBehaviour starBehaviour = _stars[index];
                _limitedSpaceChecker.CorrectPosition(starBehaviour, shipPosition, width, height);
            }
        }

        private void Shuffle(List<StarBehaviour> stars)
        {
            int count = stars.Count;
            for (var i = 0; i < count; i++)
            {
                int index = Random.Range(i, count);
                (stars[i], stars[index]) = (stars[index], stars[i]);
            }
        }
    }
}