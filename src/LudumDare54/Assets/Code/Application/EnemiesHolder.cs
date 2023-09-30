﻿using System.Collections.Generic;

namespace LudumDare54
{
    public sealed class EnemiesHolder
    {
        private readonly List<Ship> _ships = new();
        public IReadOnlyList<Ship> Ships => _ships;

        public void AddShip(Ship ship)
        {
            _ships.Add(ship);
        }
    }
}