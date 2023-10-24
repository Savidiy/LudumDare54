using UniRx;

namespace LudumDare54
{
    public sealed class LoseLevelWindow : IActivatable
    {
        private readonly LoseLevelBehaviour _loseLevelBehaviour;
        private readonly InputProvider _inputProvider;
        private readonly IEventInvoker _eventInvoker;
        private readonly ApplicationStateMachine _applicationStateMachine;
        private readonly SoundPlayer _soundPlayer;
        private readonly SoundSettings _soundSettings;
        private readonly ProgressProvider _progressProvider;
        private CompositeDisposable _subscriptions;

        public LoseLevelWindow(LoseLevelBehaviour loseLevelBehaviour, InputProvider inputProvider, IEventInvoker eventInvoker,
            ApplicationStateMachine applicationStateMachine, SoundPlayer soundPlayer, SoundSettings soundSettings,
            ProgressProvider progressProvider)
        {
            _progressProvider = progressProvider;
            _eventInvoker = eventInvoker;
            _applicationStateMachine = applicationStateMachine;
            _soundPlayer = soundPlayer;
            _soundSettings = soundSettings;
            _inputProvider = inputProvider;
            _loseLevelBehaviour = loseLevelBehaviour;
            _loseLevelBehaviour.gameObject.SetActive(false);
        }

        public void Activate()
        {
            _soundPlayer.PlayOnce(_soundSettings.LoseLevelSoundId);
            _loseLevelBehaviour.LevelNameText.text = $"Level {_progressProvider.Progress.CurrentLevelIndex + 1} failed!";
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
            if (_inputProvider.IsAnyFireDown())
                RestartLevel();
        }

        private void ReturnToMenu()
        {
            _soundPlayer.PlayClick();
            _applicationStateMachine.EnterToState<MainMenuApplicationState>();
        }

        private void RestartLevel()
        {
            _soundPlayer.PlayClick();
            _applicationStateMachine.EnterToState<LoadingLevelApplicationState>();
        }
    }
}