using UnityEngine;

namespace LudumDare54
{
    public sealed class InputProvider
    {
        private readonly InputSettings _inputSettings;

        public InputProvider(InputSettings inputSettings)
        {
            _inputSettings = inputSettings;
        }

        public HeroInputData GetMoveInput()
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

            return new HeroInputData(move, rotate, strafe);
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

        public InputShootData GetShootInput()
        {
            bool hasFire1 = IsAnyPressed(_inputSettings.Fire1);
            bool hasFire2 = IsAnyPressed(_inputSettings.Fire2);
            bool hasFire3 = IsAnyPressed(_inputSettings.Fire3);
            bool hasFire4 = IsAnyPressed(_inputSettings.Fire4);

            return new InputShootData(hasFire1, hasFire2, hasFire3, hasFire4);
        }
    }
}