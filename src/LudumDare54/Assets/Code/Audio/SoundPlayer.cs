using Savidiy.Utils;
using UniRx;
using UnityEngine;

namespace LudumDare54
{
    public sealed class SoundPlayer : DisposableCollector
    {
        private readonly AudioSource _soundSource;
        private readonly SoundLibrary _soundLibrary;
        private readonly SoundVolumeProvider _soundVolumeProvider;
        private readonly SoundSettings _soundSettings;

        public SoundPlayer(CameraProvider cameraProvider, SoundVolumeProvider soundVolumeProvider, SoundLibrary soundLibrary,
            SoundSettings soundSettings)
        {
            _soundSettings = soundSettings;
            _soundVolumeProvider = soundVolumeProvider;
            _soundLibrary = soundLibrary;

            _soundSource = cameraProvider.Camera.gameObject.AddComponent<AudioSource>();
            _soundSource.loop = false;
            _soundSource.playOnAwake = false;

            AddDisposable(soundVolumeProvider.SoundVolume.Subscribe(OnSoundVolumeChange));
        }

        public void PlayClick()
        {
            PlayOnce(_soundSettings.ClickSoundId);
        }

        public void PlayOnce(SoundIdData soundIdData)
        {
            if (_soundVolumeProvider.SoundVolume.Value == 0)
                return;

            string soundId = soundIdData.SoundId;
            if (_soundLibrary.TryGetClip(soundId, out AudioClip audioClip))
                _soundSource.PlayOneShot(audioClip);
        }

        private void OnSoundVolumeChange(float volume)
        {
            _soundSource.volume = volume;
        }
    }
}