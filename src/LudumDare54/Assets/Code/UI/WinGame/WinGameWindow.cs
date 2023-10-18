using UniRx;

namespace LudumDare54
{
    public sealed class WinGameWindow : IActivatable
    {
        private readonly WinGameBehaviour _winGameBehaviour;
        private readonly ApplicationStateMachine _applicationStateMachine;
        private readonly ProgressProvider _progressProvider;
        private readonly SoundPlayer _soundPlayer;
        private readonly SoundSettings _soundSettings;

        private CompositeDisposable _subscriptions;

        public WinGameWindow(WinGameBehaviour winGameBehaviour, ApplicationStateMachine applicationStateMachine,
            ProgressProvider progressProvider, SoundPlayer soundPlayer, SoundSettings soundSettings)
        {
            _progressProvider = progressProvider;
            _soundPlayer = soundPlayer;
            _soundSettings = soundSettings;
            _winGameBehaviour = winGameBehaviour;
            _applicationStateMachine = applicationStateMachine;
            _winGameBehaviour.gameObject.SetActive(false);
        }

        public void Activate()
        {
            _soundPlayer.PlayOnce(_soundSettings.WinGameSoundId);
            _winGameBehaviour.gameObject.SetActive(true);
            _subscriptions?.Dispose();
            _subscriptions = new CompositeDisposable();
            _subscriptions.Add(_winGameBehaviour.NewGameButton.SubscribeClick(StartNewGame));
            _winGameBehaviour.WinText.text = GetWinText();
        }

        private string GetWinText()
        {
            int bulletCount = _progressProvider.Progress.BulletCount;
            int hitCount = _progressProvider.Progress.BulletHitCount;
            float accuracy = (float) hitCount / bulletCount * 100;
            
            return $"Congratulation!\n" +
                   $"You complete game!\n\n" +
                   $"You died {_progressProvider.Progress.HeroDeathCount} times\n" +
                   $"You kill {_progressProvider.Progress.EnemiesKillCount} enemies\n" +
                   $"You ram {_progressProvider.Progress.BumperHitCount} enemies\n" +
                   $"You shoot {bulletCount} bullets\n" +
                   $"You hit {hitCount} times\n" +
                   $"Your accuracy {accuracy:F0}%";
        }

        public void Deactivate()
        {
            _winGameBehaviour.gameObject.SetActive(false);
            _subscriptions?.Dispose();
            _subscriptions = null;
        }

        private void StartNewGame()
        {
            _soundPlayer.PlayClick();
            _applicationStateMachine.EnterToState<LoadingLevelApplicationState>();
        }
    }
}