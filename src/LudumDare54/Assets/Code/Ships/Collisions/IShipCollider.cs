using UnityEngine;

namespace LudumDare54
{
    public interface IShipCollider
    {
        Collider2D Collider { get; }
    }
}