﻿using UnityEngine;

namespace LudumDare54
{
    public sealed class HeroShipHolder
    {
        private Ship _ship;
        
        public bool HasHeroShip => _ship != null;

        public bool TryGetHeroShip(out Ship ship)
        {
            ship = _ship;
            return ship != null;
        }

        public void SetHeroShip(Ship ship)
        {
            _ship = ship;
        }

        public void Clear()
        {
            _ship.Dispose();
            _ship = null;
        }

        public void RemoveHeroShip()
        {
            Debug.LogError("RemoveHeroShip is not implemented");
        }
    }
}