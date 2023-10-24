using System;

namespace LudumDare54
{
    public sealed class MainMenuInvoker : IActivatable
    {
        private readonly InputProvider _inputProvider;
        private readonly IEventInvoker _eventInvoker;
        private readonly ApplicationStateMachine _applicationStateMachine;
        private readonly SoundPlayer _soundPlayer;

        private IDisposable _updateSubscribe;

        public MainMenuInvoker(InputProvider inputProvider, IEventInvoker eventInvoker,
            ApplicationStateMachine applicationStateMachine, SoundPlayer soundPlayer)
        {
            _inputProvider = inputProvider;
            _eventInvoker = eventInvoker;
            _applicationStateMachine = applicationStateMachine;
            _soundPlayer = soundPlayer;
        }

        public void Activate()
        {
            _updateSubscribe ??= _eventInvoker.Subscribe(UnityEventType.Update, OnUpdate);
        }

        public void Deactivate()
        {
            _updateSubscribe?.Dispose();
            _updateSubscribe = null;
        }

        private void OnUpdate()
        {
            if (_inputProvider.IsMenuDown())
                ReturnToMenu();
        }

        private void ReturnToMenu()
        {
            _soundPlayer.PlayClick();
            _applicationStateMachine.EnterToState<MainMenuApplicationState>();
        }
    }
}