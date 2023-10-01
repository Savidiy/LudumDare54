using UniRx;

namespace LudumDare54
{
    public sealed class LoseLevelWindow : IActivatable
    {
        private readonly LoseLevelBehaviour _loseLevelBehaviour;
        private readonly InputProvider _inputProvider;
        private readonly IEventInvoker _eventInvoker;
        private readonly ApplicationStateMachine _applicationStateMachine;
        private CompositeDisposable _subscriptions;

        public LoseLevelWindow(LoseLevelBehaviour loseLevelBehaviour, InputProvider inputProvider, IEventInvoker eventInvoker,
            ApplicationStateMachine applicationStateMachine)
        {
            _eventInvoker = eventInvoker;
            _applicationStateMachine = applicationStateMachine;
            _inputProvider = inputProvider;
            _loseLevelBehaviour = loseLevelBehaviour;
            _loseLevelBehaviour.gameObject.SetActive(false);
        }

        public void Activate()
        {
            _loseLevelBehaviour.gameObject.SetActive(true);
            _subscriptions?.Dispose();
            _subscriptions = new CompositeDisposable();
            _subscriptions.Add(_eventInvoker.Subscribe(UnityEventType.Update, OnUpdate));
            _subscriptions.Add(_loseLevelBehaviour.MenuButton.SubscribeClick(ReturnToMenu));
            _subscriptions.Add(_loseLevelBehaviour.RestartButton.SubscribeClick(RestartLevel));
        }

        public void Deactivate()
        {
            _loseLevelBehaviour.gameObject.SetActive(false);
            _subscriptions?.Dispose();
            _subscriptions = null;
        }

        private void OnUpdate()
        {
            if (_inputProvider.IsMenuDown())
                ReturnToMenu();

            if (_inputProvider.IsAnyFireDown())
                RestartLevel();
        }

        private void ReturnToMenu()
        {
            _applicationStateMachine.EnterToState<MainMenuApplicationState>();
        }

        private void RestartLevel()
        {
            _applicationStateMachine.EnterToState<LoadingLevelApplicationState>();
        }
    }
}