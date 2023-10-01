﻿using UnityEngine;

namespace LudumDare54
{
    public sealed class ShipHealth : IShipHealth
    {
        private int _health;

        public Vector3 LastAttackVector { get; private set; }
        public int Health => _health;
        public bool IsAlive => _health > 0;
        public bool IsDead => !IsAlive;

        public ShipHealth(int maxHealth)
        {
            _health = maxHealth;
        }

        public void TakeDamage(IShipDamage damage, Vector3 attackVector)
        {
            LastAttackVector = attackVector;
            _health -= damage.Damage;
        }
    }
}