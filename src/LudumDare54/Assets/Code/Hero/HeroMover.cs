using UnityEngine;

namespace LudumDare54
{
    public sealed class HeroMover : IShipMover
    {
        private readonly ShipBehaviour _shipBehaviour;
        private readonly IShipStats _shipStats;
        private readonly PlayerInputProvider _playerInputProvider;

        public HeroMover(ShipBehaviour shipBehaviour, IShipStats shipStats, PlayerInputProvider playerInputProvider)
        {
            _shipBehaviour = shipBehaviour;
            _shipStats = shipStats;
            _playerInputProvider = playerInputProvider;
        }
        
        public void UpdatePosition(float deltaTime)
        {
            PlayerInputData playerInputData = _playerInputProvider.GetPlayerInput();
            float moveInput = playerInputData.Move;
            float rotateInput = playerInputData.Rotate;
            float strafeInput = playerInputData.Strafe;

            float rotationSpeed = _shipStats.RotationSpeed;
            float strafeSpeed = _shipStats.StrafeSpeed;
            float forwardSpeed = _shipStats.ForwardSpeed;
            float backwardSpeed = _shipStats.BackwardSpeed;
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