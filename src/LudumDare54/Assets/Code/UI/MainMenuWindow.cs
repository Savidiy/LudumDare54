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
        private readonly SoundVolumeProvider _soundVolumeProvider;
        private readonly SoundPlayer _soundPlayer;

        private CompositeDisposable _subscriptions;

        public MainMenuWindow(MainMenuBehaviour mainMenuBehaviour, InputProvider inputProvider, IEventInvoker eventInvoker,
            ApplicationStateMachine applicationStateMachine, ProgressProvider progressProvider, SoundPlayer soundPlayer,
            SoundVolumeProvider soundVolumeProvider)
        {
            _soundPlayer = soundPlayer;
            _progressProvider = progressProvider;
            _soundVolumeProvider = soundVolumeProvider;
            _mainMenuBehaviour = mainMenuBehaviour;
            _inputProvider = inputProvider;
            _eventInvoker = eventInvoker;
            _applicationStateMachine = applicationStateMachine;
            _mainMenuBehaviour.gameObject.SetActive(false);
        }

        public void Activate()
        {
            _mainMenuBehaviour.gameObject.SetActive(true);
            _mainMenuBehaviour.MusicVolumeSlider.value = _soundVolumeProvider.MusicVolume.Value;
            _mainMenuBehaviour.SoundVolumeSlider.value = _soundVolumeProvider.SoundVolume.Value;
            
            _subscriptions?.Dispose();
            _subscriptions = new CompositeDisposable();
            _subscriptions.Add(_eventInvoker.Subscribe(UnityEventType.Update, OnUpdate));
            _subscriptions.Add(_mainMenuBehaviour.StartButton.SubscribeClick(StartNewGame));
            _subscriptions.Add(_mainMenuBehaviour.ContinueButton.SubscribeClick(ContinueGame));
            _subscriptions.Add(_mainMenuBehaviour.ResetProgressButton.SubscribeClick(ResetProgress));
            _subscriptions.Add(_mainMenuBehaviour.MusicVolumeSlider.SubscribeValueChanged(MusicVolumeChanged));
            _subscriptions.Add(_mainMenuBehaviour.SoundVolumeSlider.SubscribeValueChanged(SoundVolumeChanged));

            UpdateButtonStates();
        }

        private void SoundVolumeChanged(float obj) => _soundVolumeProvider.SetSoundVolume(obj);

        private void MusicVolumeChanged(float obj) => _soundVolumeProvider.SetMusicVolume(obj);

        public void Deactivate()
        {
            _mainMenuBehaviour.gameObject.SetActive(false);
            _subscriptions?.Dispose();
            _subscriptions = null;
        }

        private void ResetProgress()
        {
            _soundPlayer.PlayClick();
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
            _soundPlayer.PlayClick();
            _progressProvider.ResetProgress();
            _applicationStateMachine.EnterToState<LoadingLevelApplicationState>();
        }

        private void ContinueGame()
        {
            _soundPlayer.PlayClick();
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