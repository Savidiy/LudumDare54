using Savidiy.Utils.StateMachine;

namespace LudumDare54
{
    public sealed class LoadingLevelApplicationState : IState, IApplicationState
    {
        private readonly ApplicationStateMachine _applicationStateMachine;
        private readonly LevelDataProvider _levelDataProvider;
        private readonly HeroShipHolder _heroShipHolder;
        private readonly ShipFactory _shipFactory;
        private readonly EnemiesHolder _enemiesHolder;

        public LoadingLevelApplicationState(ApplicationStateMachine applicationStateMachine, LevelDataProvider levelDataProvider,
            HeroShipHolder heroShipHolder, ShipFactory shipFactory, EnemiesHolder enemiesHolder)
        {
            _applicationStateMachine = applicationStateMachine;
            _levelDataProvider = levelDataProvider;
            _heroShipHolder = heroShipHolder;
            _shipFactory = shipFactory;
            _enemiesHolder = enemiesHolder;
        }

        public void Enter()
        {
            CreateHero();
            CreateEnemies();
            _applicationStateMachine.EnterToState<GameLoopApplicationState>();
        }

        private void CreateHero()
        {
            Ship heroShip = _shipFactory.CreateHeroShip();
            _heroShipHolder.SetHeroShip(heroShip);
        }

        private void CreateEnemies()
        {
            LevelStaticData levelStaticData = _levelDataProvider.GetCurrentLevel();
            foreach (SpawnPointStaticData spawnPointStaticData in levelStaticData.SpawnPoints)
            {
                Ship enemyShip = _shipFactory.CreateEnemyShip(spawnPointStaticData);
                _enemiesHolder.AddShip(enemyShip);
            }
        }
    }
}