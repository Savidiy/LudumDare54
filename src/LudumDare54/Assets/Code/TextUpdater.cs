using TMPro;
using UnityEngine;

namespace LudumDare54
{
    public sealed class TextUpdater
    {
        private readonly TMP_Text _text;
        private readonly IEventInvoker _eventInvoker;

        public TextUpdater(TMP_Text text, IEventInvoker eventInvoker)
        {
            _eventInvoker = eventInvoker;
            _text = text;
        }

        public void StartUpdate()
        {
            _eventInvoker.Subscribe(UnityEventType.Update, OnUpdate);
        }

        private void OnUpdate()
        {
            _text.text = $"{Time.time:f2}";
        }
    }
}