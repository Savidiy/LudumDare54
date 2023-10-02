using UniRx;

namespace LudumDare54
{
    public sealed class WinGameWindow : IActivatable
    {
        private readonly WinGameBehaviour _winGameBehaviour;
        private readonly ApplicationStateMachine _applicationStateMachine;
        private readonly ProgressProvider _progressProvider;

        private CompositeDisposable _subscriptions;

        public WinGameWindow(WinGameBehaviour winGameBehaviour, ApplicationStateMachine applicationStateMachine,
            ProgressProvider progressProvider)
        {
            _progressProvider = progressProvider;
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
            _applicationStateMachine.EnterToState<LoadingLevelApplicationState>();
        }
    }
}