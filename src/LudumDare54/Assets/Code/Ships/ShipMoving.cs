using System;

namespace LudumDare54
{
    public sealed class ShipMoving
    {
        private readonly IEventInvoker _eventInvoker;
        private readonly HeroShipHolder _heroShipHolder;
        private IDisposable _updateSubscribe;

        public ShipMoving(IEventInvoker eventInvoker, HeroShipHolder heroShipHolder)
        {
            _eventInvoker = eventInvoker;
            _heroShipHolder = heroShipHolder;
        }

        public void Activate()
        {
            _updateSubscribe ??= _eventInvoker.Subscribe(UnityEventType.Update, OnUpdate);
        }

        public void Deactivate()
        {
            _updateSubscribe?.Dispose();
            _updateSubscribe = null;
        }

        private void OnUpdate()
        {
            if (_heroShipHolder.TryGetHeroShip(out Ship heroShip))
                Move(heroShip);
        }

        private void Move(Ship ship)
        {
            IShipControls controls = ship.ShipControls;
            float moveControl = controls.Move;
            float rotateControl = controls.Rotate;
            float strafeControl = controls.Strafe;

            IShipStats stats = ship.Stats;
            float rotationSpeed = stats.RotationSpeed;
            float strafeSpeed = stats.StrafeSpeed;
            float forwardSpeed = stats.ForwardSpeed;
            float backwardSpeed = stats.BackwardSpeed;
            float moveSpeed = rotateControl >= 0 ? forwardSpeed : backwardSpeed;

            float deltaTime = _eventInvoker.DeltaTime;
            float rotation = moveControl * rotationSpeed * deltaTime;
            ship.Rotate(rotation);

            float movement = rotateControl * moveSpeed * deltaTime;
            ship.Move(movement);

            float control = strafeControl * strafeSpeed * deltaTime;
            ship.Strafe(control);
        }
    }
}