using UnityEngine;

namespace LudumDare54
{
    public sealed class HeroMover : IShipMover
    {
        private readonly ShipBehaviour _shipBehaviour;
        private readonly HeroStats _heroStats;
        private readonly InputProvider _inputProvider;

        public HeroMover(ShipBehaviour shipBehaviour, HeroStats heroStats, InputProvider inputProvider)
        {
            _shipBehaviour = shipBehaviour;
            _heroStats = heroStats;
            _inputProvider = inputProvider;
        }
        
        public void UpdatePosition(float deltaTime)
        {
            HeroInputData heroInputData = _inputProvider.GetMoveInput();
            float moveInput = heroInputData.Move;
            float rotateInput = heroInputData.Rotate;
            float strafeInput = heroInputData.Strafe;

            float rotationSpeed = _heroStats.RotationSpeed;
            float strafeSpeed = _heroStats.StrafeSpeed;
            float forwardSpeed = _heroStats.ForwardSpeed;
            float backwardSpeed = _heroStats.BackwardSpeed;
            float moveSpeed = rotateInput >= 0 ? forwardSpeed : backwardSpeed;

            Transform transform = _shipBehaviour.transform;

            float rotateDelta = moveInput * rotationSpeed * deltaTime;
            transform.Rotate(0, 0, -rotateDelta);

            float moveDelta = rotateInput * moveSpeed * deltaTime;
            Vector3 moveShift = transform.up * moveDelta;
            float strafeDelta = strafeInput * strafeSpeed * deltaTime;
            Vector3 strafeMove = transform.right * strafeDelta;
            transform.position += moveShift + strafeMove;
        }
    }
}