using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LudumDare54
{
    public class Bootstrap : MonoBehaviour
    {
        private ApplicationStateMachine _applicationStateMachine;

        [Inject]
        public void Construct(ApplicationStateMachine applicationStateMachine, List<IApplicationState> applicationStates)
        {
            _applicationStateMachine = applicationStateMachine;
            _applicationStateMachine.AddStates(applicationStates);
        }

        private void Awake()
        {
            _applicationStateMachine.EnterToState<LoadingLevelApplicationState>();
        }
    }
}