using System;
using UnityEngine;

namespace LudumDare54
{
    internal sealed class EventBehaviour : MonoBehaviour
    {
        private readonly EventActionList _onUpdateActions = new();
        private readonly EventActionList _onLateUpdateActions = new();
        private readonly EventActionList _onFixedUpdateActions = new();
        private readonly EventActionList _onGizmosActions = new();

        public void Add(UnityEventType eventTypeType, Action action)
        {
            EventActionList eventActionList = GetEventActionList(eventTypeType);
            eventActionList.Add(action);
        }

        public void Remove(UnityEventType eventTypeType, Action action)
        {
            EventActionList eventActionList = GetEventActionList(eventTypeType);
            eventActionList.Remove(action);
        }

        private void Update()
        {
            _onUpdateActions.InvokeActions();
        }

        private void LateUpdate()
        {
            _onLateUpdateActions.InvokeActions();
            CleanUpActions();
        }

        private void CleanUpActions()
        {
            _onUpdateActions.RemoveActions();
            _onLateUpdateActions.RemoveActions();
            _onFixedUpdateActions.RemoveActions();
            _onGizmosActions.RemoveActions();
        }

        public void Clear()
        {
            _onUpdateActions.Clear();
            _onLateUpdateActions.Clear();
            _onFixedUpdateActions.Clear();
            _onGizmosActions.Clear();
        }

        private void FixedUpdate()
        {
            _onFixedUpdateActions.InvokeActions();
        }

        private void OnDrawGizmos()
        {
            _onGizmosActions.InvokeActions();
        }

        private EventActionList GetEventActionList(UnityEventType eventTypeType)
        {
            return eventTypeType switch
            {
                UnityEventType.Update => _onUpdateActions,
                UnityEventType.FixedUpdate => _onFixedUpdateActions,
                UnityEventType.Gizmos => _onGizmosActions,
                UnityEventType.LateUpdate => _onLateUpdateActions,
                _ => throw new ArgumentOutOfRangeException(nameof(eventTypeType), eventTypeType, null)
            };
        }
    }
}