using UnityEngine;

namespace LudumDare54
{
    public class SimpleCollider : IShipCollider
    {
        public Collider2D Collider { get; }

        public SimpleCollider(ShipBehaviour shipBehaviour)
        {
            Collider = shipBehaviour.Collider;
        }
    }
}