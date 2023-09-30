using System;

namespace LudumDare54
{
    public sealed class LevelDataProvider
    {
        private readonly LevelSettings _levelSettings;
        private readonly LevelLibrary _levelLibrary;

        public LevelDataProvider(LevelSettings levelSettings, LevelLibrary levelLibrary)
        {
            _levelSettings = levelSettings;
            _levelLibrary = levelLibrary;
        }

        public LevelStaticData GetCurrentLevel()
        {
            string levelId = _levelSettings.LevelQueue[0].LevelId;
            if (_levelLibrary.TryGetLevelStaticData(levelId, out LevelStaticData levelStaticData))
            {
                return levelStaticData;
            }

            throw new Exception($"Can't find level with id '{levelId}'");
        }
    }
}