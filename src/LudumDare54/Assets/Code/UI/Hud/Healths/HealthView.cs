namespace LudumDare54
{
    public class HealthView
    {
        private static int WhitePercentProperty => SpriteHighlighter.WhitePercentProperty;
        public bool IsActive { get; private set; }

        private readonly HealthBehaviour _behaviour;
        private readonly HudHealthSettings _settings;
        private float _colorProgress = 1f;

        public HealthView(HealthBehaviour behaviour, HudHealthSettings settings)
        {
            _settings = settings;
            _behaviour = behaviour;
        }

        public void InstantActivate()
        {
            _behaviour.Image.sprite = _settings.ActiveHearth;
            _behaviour.gameObject.SetActive(true);
            IsActive = true;
        }

        public void ShowLostHealth()
        {
            _behaviour.Image.sprite = _settings.InactiveHearth;
            IsActive = false;
        }

        public void Hide()
        {
            _behaviour.gameObject.SetActive(false);
            IsActive = false;
        }

        private void SetProgress(float progress)
        {
            _colorProgress = progress;
            _behaviour.Image.SetAlpha(progress);
        }
    }
}