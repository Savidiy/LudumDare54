using System;
using UniRx;
using UnityEngine.UI;

namespace LudumDare54
{
    public static class SliderExtensions
    {
        public static IDisposable SubscribeValueChanged(this Slider slider, Action<float> onValueChanged)
        {
            return slider.OnValueChangedAsObservable().Subscribe(onValueChanged);
        }
    }
}