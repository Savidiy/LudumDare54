using System;
using Sirenix.OdinInspector;

namespace LudumDare54
{
    [Serializable, InlineProperty]
    public sealed class SoundIdData
    {
        [ValueDropdown(nameof(SoundIds)), HorizontalGroup, HideLabel] public string SoundId = string.Empty;
        private ValueDropdownList<string> SoundIds => OdinSoundIdsProvider.SoundIds;
        public static SoundIdData Empty { get; } = new() {SoundId = string.Empty};

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