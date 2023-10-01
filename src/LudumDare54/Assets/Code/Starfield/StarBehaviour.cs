using UnityEngine;

namespace LudumDare54
{
    public class StarBehaviour : MonoBehaviour, ICanShiftPosition
    {
        public SpriteRenderer SpriteRenderer;
        public float MoveMultiplier;

        public Vector3 Position => transform.position;

        public void ShiftPosition(Vector3 shift)
        {
            transform.position += shift;
        }
    }
}