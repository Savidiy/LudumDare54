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
                Color starColor = GetRandom(_starFieldSettings.StarColors);
                star.SpriteRenderer.color = starColor;
                float minStarMoveMultiplier = _starFieldSettings.MinStarMoveMultiplier;
                float maxStarMoveMultiplier = _starFieldSettings.MaxStarMoveMultiplier;
                star.MoveMultiplier = Random.Range(minStarMoveMultiplier, maxStarMoveMultiplier);
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
    }
}