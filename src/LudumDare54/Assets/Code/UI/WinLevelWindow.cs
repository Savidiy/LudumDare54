using UniRx;

namespace LudumDare54
{
    public sealed class WinLevelWindow : IActivatable
    {
        private readonly WinLevelBehaviour _winLevelBehaviour;
        private readonly InputProvider _inputProvider;
        private readonly IEventInvoker _eventInvoker;
        private readonly ApplicationStateMachine _applicationStateMachine;
        private CompositeDisposable _subscriptions;

        public WinLevelWindow(WinLevelBehaviour winLevelBehaviour, InputProvider inputProvider, IEventInvoker eventInvoker,
            ApplicationStateMachine applicationStateMachine)
        {
            _eventInvoker = eventInvoker;
            _applicationStateMachine = applicationStateMachine;
            _inputProvider = inputProvider;
            _winLevelBehaviour = winLevelBehaviour;
            _winLevelBehaviour.gameObject.SetActive(false);
        }

        public void Activate()
        {
            _winLevelBehaviour.gameObject.SetActive(true);
            _subscriptions?.Dispose();
            _subscriptions = new CompositeDisposable();
            _subscriptions.Add(_eventInvoker.Subscribe(UnityEventType.Update, OnUpdate));
            _subscriptions.Add(_winLevelBehaviour.NextLevelButton.SubscribeClick(NextLevel));
            _subscriptions.Add(_winLevelBehaviour.MenuButton.SubscribeClick(ReturnToMenu));
        }

        public void Deactivate()
        {
            _winLevelBehaviour.gameObject.SetActive(false);
            _subscriptions?.Dispose();
            _subscriptions = null;
        }

        private void OnUpdate()
        {
            if (_inputProvider.IsMenuDown())
                ReturnToMenu();

            if (_inputProvider.IsAnyFireDown())
                NextLevel();
        }

        private void ReturnToMenu()
        {
            _applicationStateMachine.EnterToState<MainMenuApplicationState>();
        }

        private void NextLevel()
        {
            _applicationStateMachine.EnterToState<LoadingLevelApplicationState>();
        }
    }
}