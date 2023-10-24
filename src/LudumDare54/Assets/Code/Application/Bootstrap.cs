using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LudumDare54
{
    public class Bootstrap : MonoBehaviour
    {
        private ApplicationStateMachine _applicationStateMachine;
        private ProgressSettings _progressSettings;
        private ProgressProvider _progressProvider;
        private MusicPlayer _musicPlayer;

        [Inject]
        public void Construct(ApplicationStateMachine applicationStateMachine, List<IApplicationState> applicationStates,
            ProgressSettings progressSettings, ProgressProvider progressProvider, MusicPlayer musicPlayer)
        {
            _musicPlayer = musicPlayer;
            _progressProvider = progressProvider;
            _progressSettings = progressSettings;
            _applicationStateMachine = applicationStateMachine;
            _applicationStateMachine.AddStates(applicationStates);
        }

        private void Awake()
        {
            _musicPlayer.PlayMusic();

#if UNITY_EDITOR
            if (_progressSettings.TestMode)
            {
                _progressProvider.ResetProgress();
                _applicationStateMachine.EnterToState<LoadingLevelApplicationState>();
                return;
            }
#endif

            _applicationStateMachine.EnterToState<MainMenuApplicationState>();
        }
    }
}