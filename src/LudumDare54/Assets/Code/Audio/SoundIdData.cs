using System;
using Savidiy.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LudumDare54
{
    [Serializable]
    public sealed class SoundIdData
    {
        [ValueDropdown(nameof(SoundIds)), InlineButton(nameof(FindSound), "Ping"), HideLabel] public string SoundId;
        private ValueDropdownList<string> SoundIds => OdinSoundIdsProvider.SoundIds;
        public static SoundIdData Empty { get; } = new() {SoundId = string.Empty};

        public static implicit operator string(SoundIdData soundIdData)
        {
            return soundIdData.SoundId;
        }

#if UNITY_EDITOR
        private static EditorScriptableObjectLoader<SoundLibrary> Loader = new();
#endif
        private void FindSound()
        {
#if UNITY_EDITOR
            if (Loader.GetAsset().TryGetClip(SoundId, out AudioClip audioClip))
            {
                UnityEditor.EditorGUIUtility.PingObject(audioClip);
            }
#endif
        }
    }
}