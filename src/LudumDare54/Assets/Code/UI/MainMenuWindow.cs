using UniRx;

namespace LudumDare54
{
    public class MainMenuWindow : IActivatable
    {
        private readonly MainMenuBehaviour _mainMenuBehaviour;
        private readonly InputProvider _inputProvider;
        private readonly IEventInvoker _eventInvoker;
        private readonly ApplicationStateMachine _applicationStateMachine;
        private readonly ProgressProvider _progressProvider;

        private CompositeDisposable _subscriptions;

        public MainMenuWindow(MainMenuBehaviour mainMenuBehaviour, InputProvider inputProvider, IEventInvoker eventInvoker,
            ApplicationStateMachine applicationStateMachine, ProgressProvider progressProvider)
        {
            _progressProvider = progressProvider;
            _mainMenuBehaviour = mainMenuBehaviour;
            _inputProvider = inputProvider;
            _eventInvoker = eventInvoker;
            _applicationStateMachine = applicationStateMachine;
            _mainMenuBehaviour.gameObject.SetActive(false);
        }

        public void Activate()
        {
            _mainMenuBehaviour.gameObject.SetActive(true);
            _subscriptions?.Dispose();
            _subscriptions = new CompositeDisposable();
            _subscriptions.Add(_eventInvoker.Subscribe(UnityEventType.Update, OnUpdate));
            _subscriptions.Add(_mainMenuBehaviour.StartButton.SubscribeClick(StartNewGame));
            _subscriptions.Add(_mainMenuBehaviour.ContinueButton.SubscribeClick(ContinueGame));
            _subscriptions.Add(_mainMenuBehaviour.ResetProgressButton.SubscribeClick(ResetProgress));

            UpdateButtonStates();
        }

        public void Deactivate()
        {
            _mainMenuBehaviour.gameObject.SetActive(false);
            _subscriptions?.Dispose();
            _subscriptions = null;
        }

        private void ResetProgress()
        {
            _progressProvider.ResetProgress();
            UpdateButtonStates();
        }

        private void OnUpdate()
        {
            if (!_inputProvider.IsAnyFireDown())
                return;

            if (_progressProvider.HasProgress)
                ContinueGame();
            else
                StartNewGame();
        }

        private void StartNewGame()
        {
            _progressProvider.ResetProgress();
            _applicationStateMachine.EnterToState<LoadingLevelApplicationState>();
        }

        private void ContinueGame()
        {
            _applicationStateMachine.EnterToState<LoadingLevelApplicationState>();
        }

        private void UpdateButtonStates()
        {
            bool hasProgress = _progressProvider.HasProgress;

            _mainMenuBehaviour.StartButton.gameObject.SetActive(!hasProgress);
            _mainMenuBehaviour.ResetProgressButton.gameObject.SetActive(hasProgress);
            _mainMenuBehaviour.ContinueButton.gameObject.SetActive(hasProgress);
        }
    }
}