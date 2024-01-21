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

        [Button("Ping", Icon = SdfIconType.Search), HorizontalGroup(Width = 60)]
        private void FindSound()
        {
            EditorAudioTester.PingAudioAsset(SoundId);
        }

        [Button("Play", Icon = SdfIconType.Play), HorizontalGroup(Width = 60)]
        private void TestSound()
        {
            EditorAudioTester.TestSound(SoundId);
        }
    }
}