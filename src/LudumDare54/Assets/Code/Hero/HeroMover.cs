using UnityEngine;

namespace LudumDare54
{
    public sealed class HeroMover : IShipMover
    {
        private readonly ShipBehaviour _shipBehaviour;
        private readonly IShipStats _shipStats;
        private readonly InputProvider _inputProvider;

        public HeroMover(ShipBehaviour shipBehaviour, IShipStats shipStats, InputProvider inputProvider)
        {
            _shipBehaviour = shipBehaviour;
            _shipStats = shipStats;
            _inputProvider = inputProvider;
        }
        
        public void UpdatePosition(float deltaTime)
        {
            HeroInputData heroInputData = _inputProvider.GetMoveInput();
            float moveInput = heroInputData.Move;
            float rotateInput = heroInputData.Rotate;
            float strafeInput = heroInputData.Strafe;

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