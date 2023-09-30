using UnityEngine;

namespace LudumDare54
{
    public interface ICanShiftPosition
    {
        void ShiftPosition(Vector3 shift);
        Vector3 Position { get; }
    }
}