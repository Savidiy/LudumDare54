namespace Savidiy.Utils
{
    public interface IPlayerPrefsService
    {
        void SetString(string key, string data);
        void SetInt(string key, int data);
        void SetFloat(string key, float data);
        string GetString(string key, string defaultValue = "");
        int GetInt(string key, int defaultValue = 0);
        float GetFloat(string key, float defaultValue = 0);
        bool HasKey(string key);
        void DeleteKey(string key);
    }
}