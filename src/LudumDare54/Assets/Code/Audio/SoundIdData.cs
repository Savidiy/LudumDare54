using System;
using Savidiy.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LudumDare54
{
    [Serializable]
    public sealed class SoundIdData
    {
        [ValueDropdown(nameof(SoundIds)), HorizontalGroup, HideLabel] public string SoundId;
        private ValueDropdownList<string> SoundIds => OdinSoundIdsProvider.SoundIds;
        public static SoundIdData Empty { get; } = new() {SoundId = string.Empty};

        public static implicit operator string(SoundIdData soundIdData)
        {
            return soundIdData.SoundId;
        }

#if UNITY_EDITOR
        private static EditorScriptableObjectLoader<SoundLibrary> Loader = new();
#endif
        [HorizontalGroup(Width = 60), Button(SdfIconType.Play, "Play")]
        private void PlaySound()
        {
#if UNITY_EDITOR
            if (!Loader.GetAsset().TryGetClip(SoundId, out AudioClip audioClip))
                return;

            Camera camera = Camera.main;
            var audioSource = camera.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioClip);
            // AudioSource.PlayClipAtPoint(audioClip, camera.transform.position);
#endif
        }

        [HorizontalGroup(Width = 60), Button(SdfIconType.PinMap, "Ping")]
        private void FindSound()
        {
#if UNITY_EDITOR
            if (Loader.GetAsset().TryGetClip(SoundId, out AudioClip audioClip))
                UnityEditor.EditorGUIUtility.PingObject(audioClip);
            else
                Debug.LogError($"Sound with id '{SoundId}' not found");
#endif
        }
    }
}