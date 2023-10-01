using UnityEngine;

namespace LudumDare54
{
    public interface IConcreteShipFactory
    {
        ShipType[] ShipTypes { get; }
        Ship Create(ShipType shipType, Vector3 position, Quaternion rotation);
    }
}