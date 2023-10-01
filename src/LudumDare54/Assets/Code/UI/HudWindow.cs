using UniRx;

namespace LudumDare54
{
    public sealed class HudWindow : IActivatable
    {
        private readonly HudBehaviour _hudBehaviour;
        private readonly ApplicationStateMachine _applicationStateMachine;
        private readonly ProgressSettings _progressSettings;
        private readonly EnemiesHolder _enemiesHolder;
        private CompositeDisposable _subscriptions;

        public HudWindow(HudBehaviour hudBehaviour, ApplicationStateMachine applicationStateMachine,
            ProgressSettings progressSettings, EnemiesHolder enemiesHolder)
        {
            _enemiesHolder = enemiesHolder;
            _hudBehaviour = hudBehaviour;
            _applicationStateMachine = applicationStateMachine;
            _progressSettings = progressSettings;
            _hudBehaviour.gameObject.SetActive(false);
        }

        public void Activate()
        {
#if UNITY_EDITOR
            _hudBehaviour.RestartButton.gameObject.SetActive(_progressSettings.TestMode);
            _hudBehaviour.KillAllButton.gameObject.SetActive(_progressSettings.TestMode);
#endif

            _hudBehaviour.gameObject.SetActive(true);
            _subscriptions?.Dispose();
            _subscriptions = new CompositeDisposable();
            _subscriptions.Add(_hudBehaviour.RestartButton.SubscribeClick(OnRestartClick));
            _subscriptions.Add(_hudBehaviour.KillAllButton.SubscribeClick(OnKillAllClick));
        }

        private void OnKillAllClick()
        {
            _enemiesHolder.Clear();
        }

        public void Deactivate()
        {
            _hudBehaviour.gameObject.SetActive(false);
            _subscriptions?.Dispose();
            _subscriptions = null;
        }

        private void OnRestartClick()
        {
            _applicationStateMachine.EnterToState<LoadingLevelApplicationState>();
        }
    }
}