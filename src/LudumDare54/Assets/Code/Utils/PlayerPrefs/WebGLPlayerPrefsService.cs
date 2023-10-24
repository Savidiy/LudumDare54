#if UNITY_WEBGL && !UNITY_EDITOR
using System.Globalization;
using System.Runtime.InteropServices;

namespace Savidiy.Utils
{
    public sealed class WebGLPlayerPrefsService : IPlayerPrefsService
    {
        private const string SAVE_PATH = "idbfs/Savidiy/";

        private string PrefixKey(string key)
        {
            return $"{SAVE_PATH}{key}";
        }

        public void SetString(string key, string data)
        {
            saveData(PrefixKey(key), data);
        }

        public void SetInt(string key, int data)
        {
            saveData(PrefixKey(key), data.ToString());
        }

        public void SetFloat(string key, float data)
        {
            saveData(PrefixKey(key), data.ToString(CultureInfo.InvariantCulture));
        }

        public string GetString(string key, string defaultValue = "")
        {
            return loadData(PrefixKey(key), defaultValue);
        }

        public int GetInt(string key, int defaultValue = 0)
        {
            string prefixKey = PrefixKey(key);
            string data = loadData(prefixKey, string.Empty);
            return data != string.Empty && int.TryParse(data, out int result)
                ? result
                : defaultValue;
        }

        public float GetFloat(string key, float defaultValue = 0f)
        {
            string prefixKey = PrefixKey(key);
            string data = loadData(prefixKey, string.Empty);
            return data != string.Empty && float.TryParse(data, NumberStyles.Any, CultureInfo.InvariantCulture, out float result)
                ? result
                : defaultValue;
        }

        public bool HasKey(string key)
        {
            string data = loadData(PrefixKey(key), string.Empty);
            return data != string.Empty;
        }

        public void DeleteKey(string key)
        {
            deleteKey(PrefixKey(key));
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
#endif