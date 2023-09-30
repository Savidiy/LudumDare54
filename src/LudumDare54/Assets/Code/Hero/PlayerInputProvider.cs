using UnityEngine;

namespace LudumDare54
{
    public sealed class PlayerInputProvider
    {
        private readonly InputSettings _inputSettings;

        public PlayerInputProvider(InputSettings inputSettings)
        {
            _inputSettings = inputSettings;
        }

        public PlayerInputData GetPlayerInput()
        {
            var move = 0;
            if (IsAnyPressed(_inputSettings.Left)) move -= 1;
            if (IsAnyPressed(_inputSettings.Right)) move += 1;

            var rotate = 0;
            if (IsAnyPressed(_inputSettings.Up)) rotate += 1;
            if (IsAnyPressed(_inputSettings.Down)) rotate -= 1;
            
            var strafe = 0;
            if (IsAnyPressed(_inputSettings.StrafeLeft)) strafe -= 1;
            if (IsAnyPressed(_inputSettings.StrafeRight)) strafe += 1;
            
            return new PlayerInputData(move, rotate, strafe);
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