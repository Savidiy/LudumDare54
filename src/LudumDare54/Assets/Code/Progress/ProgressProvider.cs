namespace LudumDare54
{
    public sealed class ProgressProvider
    {
        private readonly ProgressSettings _progressSettings;
        private readonly ProgressStorage _progressStorage;

        public bool HasProgress { get; private set; }
        public Progress Progress { get; private set; }

        public ProgressProvider(ProgressSettings progressSettings, ProgressStorage progressStorage)
        {
            _progressStorage = progressStorage;
            _progressSettings = progressSettings;
            HasProgress = _progressStorage.HasProgress();
            Progress = HasProgress
                ? _progressStorage.LoadProgress()
                : CreateDefaultProgress();
        }

        private Progress CreateDefaultProgress()
        {
            int startLevel = _progressSettings.StartLevel;
#if UNITY_EDITOR
            if (_progressSettings.TestMode)
                startLevel = _progressSettings.TestStartLevel;
#endif

            return new Progress()
            {
                CurrentLevelIndex = startLevel,
            };
        }

        public void ResetProgress()
        {
            Progress = CreateDefaultProgress();
            _progressStorage.SaveProgress(Progress);
            HasProgress = false;
        }

        public void SaveProgress()
        {
            _progressStorage.SaveProgress(Progress);
            HasProgress = true;
        }

        public void LoadProgress()
        {
            Progress = _progressStorage.LoadProgress();
            HasProgress = true;
        }
    }
}