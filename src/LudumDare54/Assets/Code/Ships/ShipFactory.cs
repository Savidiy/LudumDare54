using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LudumDare54
{
    public sealed class ShipFactory
    {
        private readonly List<IConcreteShipFactory> _factories;

        public ShipFactory(List<IConcreteShipFactory> factories)
        {
            _factories = factories;
        }

        public Ship CreateHeroShip()
        {
            return Create(ShipType.Hero, Vector3.zero, Quaternion.identity);
        }

        public Ship CreateEnemyShip(SpawnPointStaticData spawnPointStaticData)
        {
            ShipType shipType = spawnPointStaticData.ShipType;
            Vector3 position = spawnPointStaticData.Position;
            Quaternion rotation = Quaternion.Euler(spawnPointStaticData.Rotation);
            return Create(shipType, position, rotation);
        }

        public Ship Create(ShipType shipType, Vector3 position, Quaternion rotation)
        {
            for (var index = 0; index < _factories.Count; index++)
            {
                IConcreteShipFactory concreteShipFactory = _factories[index];
                if (concreteShipFactory.ShipTypes.Contains(shipType))
                    return concreteShipFactory.Create(shipType, position, rotation);
            }

            throw new Exception($"Can't find factory for ship type: {shipType}");
        }
    }
}