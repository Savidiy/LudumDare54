using System;
using UniRx;
using UnityEngine.UI;

namespace LudumDare54
{
    public static class ButtonExtensions
    {
        public static IDisposable SubscribeClick(this Button button, Action onClick)
        {
            return button.OnClickAsObservable().Subscribe(_ => onClick());
        }
    }
}