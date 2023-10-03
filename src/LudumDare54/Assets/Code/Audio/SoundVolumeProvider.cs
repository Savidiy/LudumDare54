using UniRx;
using UnityEngine;

namespace LudumDare54
{
    public class SoundVolumeProvider
    {
        private readonly ReactiveProperty<float> _musicVolume = new();
        private readonly ReactiveProperty<float> _soundVolume = new();

        private const string SOUND_VOLUME_KEY = nameof(SOUND_VOLUME_KEY);
        private const string MUSIC_VOLUME_KEY = nameof(MUSIC_VOLUME_KEY);

        public IReadOnlyReactiveProperty<float> MusicVolume => _musicVolume;
        public IReadOnlyReactiveProperty<float> SoundVolume => _soundVolume;

        public SoundVolumeProvider(SoundSettings soundSettings)
        {
            _musicVolume.Value = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, soundSettings.DefaultMusicVolume);
            _soundVolume.Value = PlayerPrefs.GetFloat(SOUND_VOLUME_KEY, soundSettings.DefaultSoundVolume);
        }

        public void SetMusicVolume(float volume)
        {
            PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
            PlayerPrefs.Save();
            _musicVolume.Value = volume;
        }

        public void SetSoundVolume(float volume)
        {
            PlayerPrefs.SetFloat(SOUND_VOLUME_KEY, volume);
            PlayerPrefs.Save();
            _soundVolume.Value = volume;
        }
    }
}