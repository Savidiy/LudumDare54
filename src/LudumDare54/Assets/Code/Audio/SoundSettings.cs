using Savidiy.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LudumDare54
{
    [CreateAssetMenu(fileName = nameof(SoundSettings), menuName = "Static Data/" + nameof(SoundSettings))]
    public sealed class SoundSettings : AutoSaveScriptableObject
    {
        public float DefaultMusicVolume = 0.4f;
        public float DefaultSoundVolume = 0.6f;
        [InlineProperty] public SoundIdData MusicSoundId;
        [InlineProperty] public SoundIdData ClickSoundId;
        [InlineProperty] public SoundIdData HeroShootSoundId;
        [InlineProperty] public SoundIdData HeroHurtSoundId;
        [InlineProperty] public SoundIdData HeroRammingSoundId;
        [InlineProperty] public SoundIdData WinLevelSoundId;
        [InlineProperty] public SoundIdData LoseLevelSoundId;
        [InlineProperty] public SoundIdData WinGameSoundId;
        [InlineProperty] public SoundIdData StartLevelSoundId;
    }
}