using UniRx;

namespace LudumDare54
{
    public sealed class WinLevelWindow : IActivatable
    {
        private readonly WinLevelBehaviour _winLevelBehaviour;
        private readonly InputProvider _inputProvider;
        private readonly IEventInvoker _eventInvoker;
        private readonly ApplicationStateMachine _applicationStateMachine;
        private readonly SoundPlayer _soundPlayer;
        private readonly SoundSettings _soundSettings;
        private readonly ProgressProvider _progressProvider;
        private CompositeDisposable _subscriptions;

        public WinLevelWindow(WinLevelBehaviour winLevelBehaviour, InputProvider inputProvider, IEventInvoker eventInvoker,
            ApplicationStateMachine applicationStateMachine, SoundPlayer soundPlayer, SoundSettings soundSettings,
            ProgressProvider progressProvider)
        {
            _eventInvoker = eventInvoker;
            _applicationStateMachine = applicationStateMachine;
            _soundPlayer = soundPlayer;
            _soundSettings = soundSettings;
            _progressProvider = progressProvider;
            _inputProvider = inputProvider;
            _winLevelBehaviour = winLevelBehaviour;
            _winLevelBehaviour.gameObject.SetActive(false);
        }

        public void Activate()
        {
            _soundPlayer.PlayOnce(_soundSettings.WinLevelSoundId);
            _winLevelBehaviour.LevelNameText.text = $"Level {_progressProvider.Progress.CurrentLevelIndex} completed!";
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
            if (_inputProvider.IsAnyFireDown())
                NextLevel();
        }

        private void ReturnToMenu()
        {
            _soundPlayer.PlayClick();
            _applicationStateMachine.EnterToState<MainMenuApplicationState>();
        }

        private void NextLevel()
        {
            _soundPlayer.PlayClick();
            _applicationStateMachine.EnterToState<LoadingLevelApplicationState>();
        }
    }
}