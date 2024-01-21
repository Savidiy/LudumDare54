using Savidiy.Utils;
using UnityEngine;

namespace LudumDare54
{
    public static class EditorAudioTester
    {
#if UNITY_EDITOR
        private static readonly EditorScriptableObjectLoader<SoundLibrary> Loader = new();
        private static AudioSource _testAudioSource;
#endif
        public static void PingAudioAsset(string audioId)
        {
#if UNITY_EDITOR
            if (Loader.GetAsset().TryGetClip(audioId, out AudioClip audioClip))
                UnityEditor.EditorGUIUtility.PingObject(audioClip);
            else
                Debug.LogError($"Sound with id '{audioId}' not found");
#endif
        }

        public static void TestSound(string audioId)
        {
#if UNITY_EDITOR
            if (Loader.GetAsset().TryGetClip(audioId, out AudioClip audioClip))
            {
                AudioSource audioSource = GetTestAudioSource();
                audioSource.clip = audioClip;
                audioSource.Play();
            }
#endif
        }

        private static AudioSource GetTestAudioSource()
        {
#if UNITY_EDITOR
            if (_testAudioSource == null)
                _testAudioSource = UnityEditor.EditorUtility
                    .CreateGameObjectWithHideFlags("Test Sound", HideFlags.HideAndDontSave, typeof(AudioSource))
                    .GetComponent<AudioSource>();
            
            return _testAudioSource;
#endif
        }
    }
}