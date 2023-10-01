using UniRx;

namespace LudumDare54
{
    public sealed class HudWindow : IActivatable
    {
        private readonly HudBehaviour _hudBehaviour;
        private readonly ApplicationStateMachine _applicationStateMachine;
        private CompositeDisposable _subscriptions;

        public HudWindow(HudBehaviour hudBehaviour, ApplicationStateMachine applicationStateMachine)
        {
            _hudBehaviour = hudBehaviour;
            _applicationStateMachine = applicationStateMachine;
            _hudBehaviour.gameObject.SetActive(false);
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
            _applicationStateMachine.EnterToState<LoadingLevelApplicationState>();
        }
    }
}