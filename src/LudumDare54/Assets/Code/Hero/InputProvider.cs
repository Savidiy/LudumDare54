using UnityEngine;

namespace LudumDare54
{
    public sealed class InputProvider : IActivatable
    {
        private readonly InputSettings _inputSettings;
        private bool _isActive = true;

        public InputProvider(InputSettings inputSettings)
        {
            _inputSettings = inputSettings;
        }

        public void Activate() => _isActive = true;
        public void Deactivate() => _isActive = false;

        public HeroInputData GetMoveInput()
        {
            if (!_isActive)
                return default;

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

        public InputShootData GetShootInput()
        {
            if (!_isActive)
                return default;

            bool hasFire1 = IsAnyPressed(_inputSettings.Fire1);
            bool hasFire2 = IsAnyPressed(_inputSettings.Fire2);
            bool hasFire3 = IsAnyPressed(_inputSettings.Fire3);
            bool hasFire4 = IsAnyPressed(_inputSettings.Fire4);

            return new InputShootData(hasFire1, hasFire2, hasFire3, hasFire4);
        }

        public bool IsMenuDown()
        {
            return IsAnyDown(_inputSettings.Menu);
        }

        public bool IsAnyFireDown()
        {
            if (IsAnyDown(_inputSettings.Fire1)) return true;
            if (IsAnyDown(_inputSettings.Fire2)) return true;
            if (IsAnyDown(_inputSettings.Fire3)) return true;
            if (IsAnyDown(_inputSettings.Fire4)) return true;
            return false;
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

        private bool IsAnyDown(KeyCode[] keyCodes)
        {
            for (var index = 0; index < keyCodes.Length; index++)
            {
                KeyCode keyCode = keyCodes[index];
                if (Input.GetKeyDown(keyCode))
                    return true;
            }

            return false;
        }
    }
}