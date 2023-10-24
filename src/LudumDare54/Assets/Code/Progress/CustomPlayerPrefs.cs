using System.Runtime.InteropServices;
using UnityEngine;

namespace LudumDare54
{
    public static class CustomPlayerPrefs
    {
        private const string SAVE_PATH = "idbfs/LD54/";

        public static string PrefixKey(string key)
        {
            return $"{SAVE_PATH}{key}";
        }

        public static void SetString(string key, string data)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            saveData(PrefixKey(key), data);
#else
            PlayerPrefs.SetString(key, data);
#endif
        }

        public static void SetInt(string key, int data)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            saveData(PrefixKey(key), data.ToString());
#else
            PlayerPrefs.SetInt(key, data);
#endif
        }

        public static void SetFloat(string key, float data)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            saveData(PrefixKey(key), data.ToString());
#else
            PlayerPrefs.SetFloat(key, data);
#endif
        }


        public static string GetString(string key, string defaultValue = "")
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            return loadData(PrefixKey(key), defaultValue);
#else
            return PlayerPrefs.GetString(key, defaultValue);
#endif
        }

        public static int GetInt(string key, int defaultValue = 0)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            var data = loadData(PrefixKey(key), defaultValue.ToString());
            if(data != string.Empty){
                return int.Parse(loadData(PrefixKey(key)));
            }

            return defaultValue;
#else
            return PlayerPrefs.GetInt(key, defaultValue);
#endif
        }

        public static float GetFloat(string key, float defaultValue = 0)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            var data = loadData(PrefixKey(key), defaultValue.ToString());
            if(data != string.Empty){
                return float.Parse(loadData(PrefixKey(key)));
            }

            return defaultValue;
#else
            return PlayerPrefs.GetFloat(key, defaultValue);
#endif
        }

        public static bool HasKey(string key)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            var data = loadData(PrefixKey(key), string.Empty);
            return data != string.Empty;
#else
            return PlayerPrefs.HasKey(key);
#endif
        }

        public static void DeleteKey(string key)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            deleteKey(PrefixKey(key));
#else
            PlayerPrefs.DeleteKey(key);
#endif
        }

        [DllImport("__Internal")]
        private static extern void saveData(string key, string data);

        [DllImport("__Internal")]
        private static extern string loadData(string key, string defaultValue = "");

        [DllImport("__Internal")]
        private static extern string deleteKey(string key);

        [DllImport("__Internal")]
        private static extern string deleteAllKeys(string prefix);
    }
}