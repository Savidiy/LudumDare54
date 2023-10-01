using Savidiy.Utils;
using UnityEngine;

namespace LudumDare54
{
    public sealed class ProgressProvider
    {
        private const string PROGRESS_KEY = "Progress";
        private readonly Serializer<Progress> _serializer;
        private readonly ProgressSettings _progressSettings;
        
        public bool HasProgress { get; private set; }
        public Progress Progress { get; private set; }

        public ProgressProvider(ProgressSettings progressSettings)
        {
            _progressSettings = progressSettings;
            _serializer = new Serializer<Progress>();
            if (PlayerPrefs.HasKey(PROGRESS_KEY))
            {
                string json = PlayerPrefs.GetString(PROGRESS_KEY);
                Progress = _serializer.Deserialize(json);
            }
            else
            {
                Progress = CreateDefaultProgress();
            }
        }

        private Progress CreateDefaultProgress()
        {
            return new Progress()
            {
                CurrentLevelIndex = _progressSettings.StartLevel,
            };
        }

        public void ResetProgress()
        {
            Progress = CreateDefaultProgress();
            SaveProgress();
            HasProgress = false;
        }

        public void SaveProgress()
        {
            string json = _serializer.Serialize(Progress);
            PlayerPrefs.SetString(PROGRESS_KEY, json);
            HasProgress = true;
        }
    }
}