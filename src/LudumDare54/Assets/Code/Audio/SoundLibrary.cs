using System;
using System.Collections.Generic;
using Savidiy.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LudumDare54
{
    [CreateAssetMenu(fileName = nameof(SoundLibrary), menuName = "Static Data/" + nameof(SoundLibrary))]
    public sealed class SoundLibrary : AutoSaveScriptableObject
    {
        [SerializeField, ListDrawerSettings(ListElementLabelName = "@this")]
        private List<AudioClipData> AudioClips = new();

        public ValueDropdownList<string> SoundIds { get; } = new();

        protected override void OnValidate()
        {
            base.OnValidate();
            SoundIds.Clear();
            for (int i = 0; i < AudioClips.Count; i++)
            {
                AudioClipData audioClipData = AudioClips[i];
                SoundIds.Add(audioClipData.SoundId);
            }
        }

        public bool TryGetClip(string soundId, out AudioClip audioClip)
        {
            audioClip = null;
            if (string.IsNullOrEmpty(soundId))
                return false;
            
            for (int i = 0; i < AudioClips.Count; i++)
            {
                AudioClipData audioClipData = AudioClips[i];
                if (audioClipData.SoundId.Equals(soundId))
                {
                    audioClip = audioClipData.AudioClip;
                    return true;
                }
            }

            Debug.LogError($"Can't find audio clip with id '{soundId}'");
            return false;
        }

        [Button]
        private void FindAllAudioClips()
        {
#if UNITY_EDITOR
            AudioClips.Clear();
            string[] guids = UnityEditor.AssetDatabase.FindAssets("t:AudioClip");
            for (int i = 0; i < guids.Length; i++)
            {
                string guid = guids[i];
                string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
                AudioClip audioClip = UnityEditor.AssetDatabase.LoadAssetAtPath<AudioClip>(path);
                if (audioClip == null)
                    continue;

                AudioClipData audioClipData = new AudioClipData
                {
                    SoundId = audioClip.name,
                    AudioClip = audioClip
                };

                AudioClips.Add(audioClipData);
            }

            ValidateAndSave();
#endif
        }
    }

    [Serializable]
    internal sealed class AudioClipData
    {
        public string SoundId = "";
        public AudioClip AudioClip;

        public override string ToString()
        {
            return AudioClip == null ? $"{SoundId}" : $"{SoundId} - {AudioClip.name}";
        }
    }
}