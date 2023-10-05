using UnityEngine;
using UnityEngine.UI;

namespace LudumDare54
{
    public sealed class HudBehaviour : MonoBehaviour
    {
        public Button RestartButton;
        public Button KillAllButton;
        public Image TemperatureBar;
        public Color MinTemperatureColor;
        public Color MiddleTemperatureColor;
        public Color MaxTemperatureColor;
        public RadarBehaviour RadarBehaviour;
    }
}