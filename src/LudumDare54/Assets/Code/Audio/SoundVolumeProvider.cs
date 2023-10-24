using Savidiy.Utils;
using UniRx;

namespace LudumDare54
{
    public class SoundVolumeProvider
    {
        private readonly IPlayerPrefsService _playerPrefsService;
        private readonly ReactiveProperty<float> _musicVolume = new();
        private readonly ReactiveProperty<float> _soundVolume = new();

        private const string SOUND_VOLUME_KEY = nameof(SOUND_VOLUME_KEY);
        private const string MUSIC_VOLUME_KEY = nameof(MUSIC_VOLUME_KEY);

        public IReadOnlyReactiveProperty<float> MusicVolume => _musicVolume;
        public IReadOnlyReactiveProperty<float> SoundVolume => _soundVolume;

        public SoundVolumeProvider(SoundSettings soundSettings, IPlayerPrefsService playerPrefsService)
        {
            _playerPrefsService = playerPrefsService;
            _musicVolume.Value = _playerPrefsService.GetFloat(MUSIC_VOLUME_KEY, soundSettings.DefaultMusicVolume);
            _soundVolume.Value = _playerPrefsService.GetFloat(SOUND_VOLUME_KEY, soundSettings.DefaultSoundVolume);
        }

        public void SetMusicVolume(float volume)
        {
            _playerPrefsService.SetFloat(MUSIC_VOLUME_KEY, volume);
            _musicVolume.Value = volume;
        }

        public void SetSoundVolume(float volume)
        {
            _playerPrefsService.SetFloat(SOUND_VOLUME_KEY, volume);
            _soundVolume.Value = volume;
        }
    }
}