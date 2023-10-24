using UnityEngine;

namespace Savidiy.Utils
{
    public sealed class DefaultPlayerPrefsService : IPlayerPrefsService
    {
        public void SetString(string key, string data)
        {
            PlayerPrefs.SetString(key, data);
        }

        public void SetInt(string key, int data)
        {
            PlayerPrefs.SetInt(key, data);
        }

        public void SetFloat(string key, float data)
        {
            PlayerPrefs.SetFloat(key, data);
        }

        public string GetString(string key, string defaultValue = "")
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }

        public int GetInt(string key, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }

        public float GetFloat(string key, float defaultValue = 0)
        {
            return PlayerPrefs.GetFloat(key, defaultValue);
        }

        public bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        public void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }
    }
}