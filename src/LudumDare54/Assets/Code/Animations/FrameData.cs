using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LudumDare54
{
    [Serializable]
    public class FrameData
    {
        [HorizontalGroup, LabelWidth(38)] public Sprite Sprite;

        [HorizontalGroup(Width = 70), LabelText("Custom"), LabelWidth(47)]
        [Tooltip("Custom duration in seconds")] public bool UseCustomDuration;

        [HorizontalGroup(Width = 70), HideLabel, ShowIf(nameof(UseCustomDuration))] public float CustomDuration = 0.1f;
    }
}