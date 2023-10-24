using UnityEngine;

namespace LudumDare54
{
    public class StarBehaviour : MonoBehaviour, ICanShiftPosition
    {
        private static readonly int RedColor = Shader.PropertyToID("_RedColor");
        private static readonly int BlueColor = Shader.PropertyToID("_BlueColor");
        private Color[] _colors;

        public SpriteRenderer SpriteRenderer;

        public float MoveMultiplier { get; private set; }
        public Vector3 Position => transform.position;

        public void Initialize(Color[] colors, float moveMultiplier)
        {
            _colors = colors;
            MoveMultiplier = moveMultiplier;
            SetRandomColor();
        }

        public void ShiftPosition(Vector3 shift)
        {
            transform.position += shift;
        }

        public void SetRandomColor()
        {
            SpriteRenderer.material.SetColor(RedColor, GetRandomColor());
            SpriteRenderer.material.SetColor(BlueColor, GetRandomColor());
        }

        private Color GetRandomColor() => _colors[Random.Range(0, _colors.Length)];
    }
}