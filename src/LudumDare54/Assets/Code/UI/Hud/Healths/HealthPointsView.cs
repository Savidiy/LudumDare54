using System.Collections.Generic;

namespace LudumDare54
{
    public sealed class HealthPointsView
    {
        private readonly HealthPointsBehaviour _behaviour;
        private readonly HealthViewFactory _healthViewFactory;
        private readonly HeroShipHolder _heroShipHolder;
        private readonly List<HealthView> _hearthViews = new();
        private int _maxHealth;
        private int _currentHealth = -1;

        public HealthPointsView(HealthPointsBehaviour behaviour, HealthViewFactory healthViewFactory,
            HeroShipHolder heroShipHolder)
        {
            _behaviour = behaviour;
            _healthViewFactory = healthViewFactory;
            _heroShipHolder = heroShipHolder;
        }

        public void ResetHealth()
        {
            _maxHealth = _heroShipHolder.TryGetHeroShip(out Ship ship) ? ship.Health.MaxHealth : _maxHealth;
            _currentHealth = _maxHealth;

            for (int i = _hearthViews.Count; i < _maxHealth; i++)
            {
                HealthView healthView = _healthViewFactory.Create(_behaviour.HealthsRoot);
                _hearthViews.Add(healthView);
            }

            for (var index = 0; index < _hearthViews.Count; index++)
            {
                HealthView healthView = _hearthViews[index];
                bool isActive = index < _maxHealth;
                if (isActive)
                    healthView.InstantActivate();
                else
                    healthView.Hide();
            }
        }

        public void UpdateHealth()
        {
            int health = _heroShipHolder.TryGetHeroShip(out Ship ship) ? ship.Health.Health : 0;
            if (health == _currentHealth)
                return;
            
            _currentHealth = health;
            for (int index = _currentHealth; index < _hearthViews.Count; index++)
            {
                HealthView healthView = _hearthViews[index];
                if (healthView.IsActive)
                    healthView.ShowLostHealth();
            }
        }
    }
}