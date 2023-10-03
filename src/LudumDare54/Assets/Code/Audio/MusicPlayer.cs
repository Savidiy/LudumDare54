using Savidiy.Utils;
using UniRx;
using UnityEngine;

namespace LudumDare54
{
    public sealed class MusicPlayer : DisposableCollector
    {
        private readonly CameraProvider _cameraProvider;
        private readonly SoundVolumeProvider _soundVolumeProvider;
        private readonly SoundLibrary _soundLibrary;
        private readonly SoundSettings _soundSettings;
        private AudioSource _musicSource;

        public MusicPlayer(CameraProvider cameraProvider, SoundVolumeProvider soundVolumeProvider, SoundLibrary soundLibrary,
            SoundSettings soundSettings)
        {
            _cameraProvider = cameraProvider;
            _soundVolumeProvider = soundVolumeProvider;
            _soundLibrary = soundLibrary;
            _soundSettings = soundSettings;
        }

        public void PlayMusic()
        {
            if (_musicSource != null)
                return;

            if (!_soundLibrary.TryGetClip(_soundSettings.MusicSoundId, out AudioClip audioClip))
                return;

            _musicSource = _cameraProvider.Camera.gameObject.AddComponent<AudioSource>();
            _musicSource.clip = audioClip;
            _musicSource.loop = true;
            _musicSource.playOnAwake = false;
            AddDisposable(_soundVolumeProvider.MusicVolume.Subscribe(OnMusicVolumeChange));
        }

        private void OnMusicVolumeChange(float volume)
        {
            _musicSource.volume = volume;

            if (volume == 0)
                _musicSource.Stop();
            else if (!_musicSource.isPlaying)
                _musicSource.Play();
        }
    }
}