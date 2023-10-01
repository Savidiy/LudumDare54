using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace LudumDare54
{
    internal sealed class DeathSpawnAction : IDeathAction
    {
        private readonly ShipBehaviour _shipBehaviour;
        private readonly ShipHealth _shipHealth;
        private readonly ShipFactory _shipFactory;
        private readonly EnemiesHolder _enemiesHolder;
        private readonly Random _random;
        private readonly List<DeathSpawnStaticData> _spawnStaticDatas;
        private readonly int _minSpawnCount;

        public DeathSpawnAction(int minSpawnCount, List<DeathSpawnStaticData> spawnStaticDatas, ShipBehaviour shipBehaviour,
            ShipHealth shipHealth, ShipFactory shipFactory, EnemiesHolder enemiesHolder)
        {
            _shipBehaviour = shipBehaviour;
            _shipHealth = shipHealth;
            _shipFactory = shipFactory;
            _enemiesHolder = enemiesHolder;
            _random = new Random();
            _spawnStaticDatas = spawnStaticDatas;
            _minSpawnCount = minSpawnCount;
        }

        public void Invoke()
        {
            Vector3 startPosition = _shipBehaviour.transform.position;
            Vector3 lastAttackVector = _shipHealth.LastAttackVector;
            float attackRotation = CalcAttackRotation(lastAttackVector);

            int count = _spawnStaticDatas.Count;
            int[] guaranteedSpawnIndexes = CalcGuaranteedSpawnIndexes(_minSpawnCount, count);
            for (var index = 0; index < _spawnStaticDatas.Count; index++)
            {
                DeathSpawnStaticData staticData = _spawnStaticDatas[index];
                float spawnChance = staticData.SpawnChance;
                bool isGuaranteedSpawn = guaranteedSpawnIndexes.Contains(index);
                bool isRandomSpawn = _random.Next(100) < spawnChance;
                bool needSpawn = isGuaranteedSpawn || isRandomSpawn;
                if (!needSpawn)
                    continue;

                ShipType shipType = staticData.ShipType;
                Vector3 rotation = GetRandomRotation(staticData, attackRotation);
                Quaternion quaternion = Quaternion.Euler(rotation);
                Vector3 shift = quaternion * (Vector3.up * staticData.PositionOffset);
                Vector3 spawnPosition = startPosition + shift;
                Ship enemyShip = _shipFactory.Create(shipType, spawnPosition, quaternion);
                _enemiesHolder.AddShip(enemyShip);
            }
        }

        private float CalcAttackRotation(Vector3 lastAttackVector)
        {
            float attackRotation = Mathf.Atan2(lastAttackVector.x, lastAttackVector.y) * Mathf.Rad2Deg;
            return attackRotation;
        }

        private Vector3 GetRandomRotation(DeathSpawnStaticData staticData, float attackRotation)
        {
            var i = (int) staticData.MinMaxAngleRandom.x;
            var i1 = (int) staticData.MinMaxAngleRandom.y;
            int rotation = _random.Next(i, i1);
            return new Vector3(0, 0, rotation + attackRotation);
        }

        private int[] CalcGuaranteedSpawnIndexes(int minSpawnCount, int elementCount)
        {
            var ints = new int[elementCount];
            for (int i = 0; i < elementCount; i++)
                ints[i] = i;

            if (elementCount < minSpawnCount)
                return ints;

            Shuffle(ints);

            var result = new int[minSpawnCount];
            for (int i = 0; i < minSpawnCount; i++)
            {
                result[i] = ints[i];
            }

            return result;
        }

        private void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int r = _random.Next(i + 1);
                (list[r], list[i]) = (list[i], list[r]);
            }
        }
    }
}