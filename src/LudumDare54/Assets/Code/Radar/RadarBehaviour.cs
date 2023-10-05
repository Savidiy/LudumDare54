using System.Collections.Generic;
using UnityEngine;

namespace LudumDare54
{
    public sealed class RadarBehaviour : MonoBehaviour
    {
        [SerializeField] private List<RadarLightBehaviour> Lights = new();

        public void SetLightProgress(IReadOnlyList<float> progress)
        {
            for (int i = 0; i < progress.Count; i++)
            {
                if (i >= Lights.Count)
                    break;

                RadarLightBehaviour radarLightBehaviour = Lights[i];
                radarLightBehaviour.SetImageProgress(progress[i]);
            }
        }
    }
}