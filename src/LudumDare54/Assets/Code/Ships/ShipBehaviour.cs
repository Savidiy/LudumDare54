using Sirenix.OdinInspector;
using UnityEngine;

namespace LudumDare54
{
    public sealed class ShipBehaviour : MonoBehaviour
    {
        [Required] public Transform RotateRoot;
        [Required] public GunBehaviour GunBehaviour;
        [Required] public Collider2D Collider;
        [Required] public SpriteHighlighter ShipHighlighter;
        [Required] public GameObject[] BlinkGameObjects;
        [Required] public IlluminatedRenderer[] IlluminatedRenderers;

        private void OnValidate()
        {
            if (RotateRoot == null)
                RotateRoot = transform;

            if (IlluminatedRenderers == null || IlluminatedRenderers.Length == 0)
                IlluminatedRenderers = GetComponentsInChildren<IlluminatedRenderer>();

            if (GunBehaviour == null)
                GunBehaviour = GetComponent<GunBehaviour>();

            if (GunBehaviour == null)
                GunBehaviour = gameObject.AddComponent<GunBehaviour>();
        }
    }
}