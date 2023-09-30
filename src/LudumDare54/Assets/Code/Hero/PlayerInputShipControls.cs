using System;
using UnityEngine;

namespace LudumDare54
{
    public sealed class PlayerInputShipControls : IShipControls
    {
        private readonly IEventInvoker _eventInvoker;
        private readonly InputSettings _inputSettings;
        private IDisposable _updateSubscribe;

        public float Move { get; private set; }
        public float Rotate { get; private set; }
        public float Strafe { get; private set; }

        public PlayerInputShipControls(IEventInvoker eventInvoker, InputSettings inputSettings)
        {
            _eventInvoker = eventInvoker;
            _inputSettings = inputSettings;
        }

        public void Activate()
        {
            _updateSubscribe ??= _eventInvoker.Subscribe(UnityEventType.Update, OnUpdate);
            OnUpdate();
        }

        public void Deactivate()
        {
            _updateSubscribe?.Dispose();
            _updateSubscribe = null;
            Move = 0;
            Rotate = 0;
            Strafe = 0;
        }

        private void OnUpdate()
        {
            Move = 0;
            if (IsAnyPressed(_inputSettings.Left)) Move -= 1;
            if (IsAnyPressed(_inputSettings.Right)) Move += 1;

            Rotate = 0;
            if (IsAnyPressed(_inputSettings.Up)) Rotate += 1;
            if (IsAnyPressed(_inputSettings.Down)) Rotate -= 1;
            
            Strafe = 0;
            if (IsAnyPressed(_inputSettings.StrafeLeft)) Strafe -= 1;
            if (IsAnyPressed(_inputSettings.StrafeRight)) Strafe += 1;
        }

        private bool IsAnyPressed(KeyCode[] keyCodes)
        {
            for (var index = 0; index < keyCodes.Length; index++)
            {
                KeyCode keyCode = keyCodes[index];
                if (Input.GetKey(keyCode))
                    return true;
            }

            return false;
        }
    }
}