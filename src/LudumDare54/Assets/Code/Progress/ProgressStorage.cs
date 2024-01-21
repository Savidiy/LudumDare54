using System;
using Savidiy.Utils;
using UnityEngine;

namespace LudumDare54
{
    public sealed class ProgressStorage
    {
        private readonly IPlayerPrefsService _playerPrefsService;
        private const string PROGRESS_KEY = "Progress";
        private readonly Serializer<Progress> _serializer = new();

        public ProgressStorage(IPlayerPrefsService playerPrefsService)
        {
            _playerPrefsService = playerPrefsService;
        }
        
        public bool HasProgress()
        {
            return _playerPrefsService.HasKey(PROGRESS_KEY);
        }

        public void SaveProgress(Progress progress)
        {
            string json = _serializer.Serialize(progress);
            _playerPrefsService.SetString(PROGRESS_KEY, json);
        }
        
        public bool TryLoadProgress(out Progress progress)
        {
            progress = default;
            
            if (!HasProgress())
                return false;

            string json = _playerPrefsService.GetString(PROGRESS_KEY);
            try
            {
                progress = _serializer.Deserialize(json);
                return true;

            }
            catch (Exception e)
            {
                Debug.LogError("Can't load progress: " + e.Message);
                return false;
            }
        }
    }
}