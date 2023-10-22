using System;
using System.Collections.Generic;

namespace LudumDare54
{
    [Serializable]
    public sealed class AnimationData
    {
        public static AnimationData Empty { get; } = new();

        public float Speed = 1f;
        public float DefaultFrameDuration = 0.1f;

        public List<FrameData> Frames = new();
    }
}