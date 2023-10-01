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
        private readonly BulletHolder _bulletHolder;

        public LoadingLevelApplicationState(ApplicationStateMachine applicationStateMachine, LevelDataProvider levelDataProvider,
            HeroShipHolder heroShipHolder, ShipFactory shipFactory, EnemiesHolder enemiesHolder, BulletHolder bulletHolder)
        {
            _applicationStateMachine = applicationStateMachine;
            _levelDataProvider = levelDataProvider;
            _heroShipHolder = heroShipHolder;
            _shipFactory = shipFactory;
            _enemiesHolder = enemiesHolder;
            _bulletHolder = bulletHolder;
        }

        public void Enter()
        {
            CleanUp();
            CreateHero();
            CreateEnemies();
            _applicationStateMachine.EnterToState<GameLoopApplicationState>();
        }

        private void CleanUp()
        {
            _enemiesHolder.Clear();
            _heroShipHolder.Clear();
            _bulletHolder.Clear();
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