using Savidiy.Utils;
using UnityEngine;

namespace LudumDare54
{
    [CreateAssetMenu(fileName = nameof(SoundSettings), menuName = "Static Data/" + nameof(SoundSettings))]
    public sealed class SoundSettings : AutoSaveScriptableObject
    {
        public float DefaultMusicVolume = 0.4f;
        public float DefaultSoundVolume = 0.6f;
        public SoundIdData MusicSoundId;
        public SoundIdData ClickSoundId;
        public SoundIdData HeroShootSoundId;
        public SoundIdData HeroHurtSoundId;
        public SoundIdData HeroRammingSoundId;
        public SoundIdData WinLevelSoundId;
        public SoundIdData LoseLevelSoundId;
        public SoundIdData WinGameSoundId;
        public SoundIdData StartLevelSoundId;
    }
}