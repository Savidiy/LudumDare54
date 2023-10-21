using UniRx;
using Zenject;
using Color = UnityEngine.Color;

namespace LudumDare54
{
    public sealed class HudWindow : IActivatable
    {
        private readonly HudBehaviour _hudBehaviour;
        private readonly ApplicationStateMachine _applicationStateMachine;
        private readonly HeroShipHolder _heroShipHolder;
        private readonly ProgressSettings _progressSettings;
        private readonly EnemiesHolder _enemiesHolder;
        private readonly IEventInvoker _eventInvoker;
        private readonly SoundPlayer _soundPlayer;
        private readonly Radar _radar;
        private readonly HealthPointsView _healthPointsView;
        private CompositeDisposable _subscriptions;

        public HudWindow(HudBehaviour hudBehaviour, ApplicationStateMachine applicationStateMachine, HeroShipHolder heroShipHolder,
            ProgressSettings progressSettings, EnemiesHolder enemiesHolder, IEventInvoker eventInvoker, SoundPlayer soundPlayer,
            Radar radar, IInstantiator instantiator)
        {
            _enemiesHolder = enemiesHolder;
            _eventInvoker = eventInvoker;
            _soundPlayer = soundPlayer;
            _radar = radar;
            _hudBehaviour = hudBehaviour;
            _applicationStateMachine = applicationStateMachine;
            _heroShipHolder = heroShipHolder;
            _progressSettings = progressSettings;
            _hudBehaviour.gameObject.SetActive(false);
            _hudBehaviour.RestartButton.gameObject.SetActive(false);
            _hudBehaviour.KillAllButton.gameObject.SetActive(false);

            _healthPointsView = instantiator.Instantiate<HealthPointsView>(new object[] {hudBehaviour.HealthPointsBehaviour});
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
            _subscriptions.Add(_eventInvoker.Subscribe(UnityEventType.Update, OnUpdate));
        }

        public void ResetHud()
        {
            _healthPointsView.ResetHealth();
        }

        private void OnUpdate()
        {
            var progress = 0f;

            if (_heroShipHolder.TryGetHeroShip(out Ship heroShip) && heroShip.ShipShooter is IHasTemperature hasTemperature)
                progress = hasTemperature.GetTemperaturePercent();

            _hudBehaviour.TemperatureBar.fillAmount = progress;
            _hudBehaviour.TemperatureBar.color = GetTemperatureBarColor(progress);
            _hudBehaviour.RadarBehaviour.SetLightProgress(_radar.RadarProgress);
            _healthPointsView.UpdateHealth();
        }

        private Color GetTemperatureBarColor(float progress)
        {
            return progress > 0.5
                ? Color.Lerp(_hudBehaviour.MiddleTemperatureColor, _hudBehaviour.MaxTemperatureColor, progress * 2 - 1)
                : Color.Lerp(_hudBehaviour.MinTemperatureColor, _hudBehaviour.MiddleTemperatureColor, progress * 2);
        }

        private void OnKillAllClick()
        {
            _soundPlayer.PlayClick();
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
            _soundPlayer.PlayClick();
            _applicationStateMachine.EnterToState<LoadingLevelApplicationState>();
        }
    }
}