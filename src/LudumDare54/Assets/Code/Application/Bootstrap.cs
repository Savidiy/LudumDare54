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

        [Inject]
        public void Construct(ApplicationStateMachine applicationStateMachine, List<IApplicationState> applicationStates,
            ProgressSettings progressSettings, ProgressProvider progressProvider)
        {
            _progressProvider = progressProvider;
            _progressSettings = progressSettings;
            _applicationStateMachine = applicationStateMachine;
            _applicationStateMachine.AddStates(applicationStates);
        }

        private void Awake()
        {
#if UNITY_EDITOR
            if (_progressSettings.SkipMenuInEditor)
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