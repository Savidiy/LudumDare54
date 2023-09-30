using UniRx;

namespace LudumDare54
{
    public sealed class HudSwitcher : IActivatable
    {
        private readonly HudBehaviour _hudBehaviour;
        private readonly ApplicationStateMachine _applicationStateMachine;
        private CompositeDisposable _subscriptions;

        public HudSwitcher(HudBehaviour hudBehaviour, ApplicationStateMachine applicationStateMachine)
        {
            _hudBehaviour = hudBehaviour;
            _applicationStateMachine = applicationStateMachine;
        }

        public void Activate()
        {
            _hudBehaviour.gameObject.SetActive(true);
            _subscriptions?.Dispose();
            _subscriptions = new CompositeDisposable();
            _subscriptions.Add(_hudBehaviour.RestartButton.SubscribeClick(OnRestartClick));
        }

        public void Deactivate()
        {
            _hudBehaviour.gameObject.SetActive(false);
            _subscriptions?.Dispose();
            _subscriptions = null;
        }

        private void OnRestartClick()
        {
            _applicationStateMachine.EnterToState<UnloadingLevelApplicationState>();
        }
    }
}