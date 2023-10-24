using Savidiy.Utils;

namespace LudumDare54
{
    public sealed class ProgressStorage
    {
        private const string PROGRESS_KEY = "Progress";
        private readonly Serializer<Progress> _serializer = new();

        public bool HasProgress()
        {
            return CustomPlayerPrefs.HasKey(PROGRESS_KEY);
        }

        public void SaveProgress(Progress progress)
        {
            string json = _serializer.Serialize(progress);
            CustomPlayerPrefs.SetString(PROGRESS_KEY, json);
        }

        public Progress LoadProgress()
        {
            string json = CustomPlayerPrefs.GetString(PROGRESS_KEY);
            Progress progress = _serializer.Deserialize(json);
            return progress;
        }
    }
}