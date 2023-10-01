using UniRx;

namespace LudumDare54
{
    public sealed class WinGameWindow : IActivatable
    {
        private readonly WinGameBehaviour _winGameBehaviour;
        private readonly ApplicationStateMachine _applicationStateMachine;

        private CompositeDisposable _subscriptions;

        public WinGameWindow(WinGameBehaviour winGameBehaviour, ApplicationStateMachine applicationStateMachine)
        {
            _winGameBehaviour = winGameBehaviour;
            _applicationStateMachine = applicationStateMachine;
            _winGameBehaviour.gameObject.SetActive(false);
        }

        public void Activate()
        {
            _winGameBehaviour.gameObject.SetActive(true);
            _subscriptions?.Dispose();
            _subscriptions = new CompositeDisposable();
            _subscriptions.Add(_winGameBehaviour.NewGameButton.SubscribeClick(StartNewGame));
        }

        public void Deactivate()
        {
            _winGameBehaviour.gameObject.SetActive(false);
            _subscriptions?.Dispose();
            _subscriptions = null;
        }

        private void StartNewGame()
        {
            _applicationStateMachine.EnterToState<LoadingLevelApplicationState>();
        }
    }
}