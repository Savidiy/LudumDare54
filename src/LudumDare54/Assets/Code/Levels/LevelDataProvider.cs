using System;

namespace LudumDare54
{
    public sealed class LevelDataProvider
    {
        private readonly LevelSettings _levelSettings;
        private readonly LevelLibrary _levelLibrary;
        private readonly ProgressProvider _progressProvider;

        public LevelDataProvider(LevelSettings levelSettings, LevelLibrary levelLibrary, ProgressProvider progressProvider)
        {
            _levelSettings = levelSettings;
            _levelLibrary = levelLibrary;
            _progressProvider = progressProvider;
        }

        public bool HasNextLevel()
        {
            return _levelSettings.LevelQueue.Count > _progressProvider.Progress.CurrentLevelIndex + 1;
        }

        public LevelStaticData GetCurrentLevel()
        {
            int levelIndex = _progressProvider.Progress.CurrentLevelIndex;

            if (levelIndex >= _levelSettings.LevelQueue.Count)
                levelIndex = _levelSettings.LevelQueue.Count - 1;

            string levelId = _levelSettings.LevelQueue[levelIndex].LevelId;
            if (_levelLibrary.TryGetLevelStaticData(levelId, out LevelStaticData levelStaticData))
                return levelStaticData;

            throw new Exception($"Can't find level with id '{levelId}'");
        }
    }
}