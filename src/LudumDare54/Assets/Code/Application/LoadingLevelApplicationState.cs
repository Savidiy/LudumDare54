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
        private readonly StarField _starField;
        private readonly ProgressProvider _progressProvider;
        private readonly HudWindow _hudWindow;
        private readonly EffectStarter _effectStarter;

        public LoadingLevelApplicationState(ApplicationStateMachine applicationStateMachine, LevelDataProvider levelDataProvider,
            HeroShipHolder heroShipHolder, ShipFactory shipFactory, EnemiesHolder enemiesHolder, BulletHolder bulletHolder,
            StarField starField, ProgressProvider progressProvider, HudWindow hudWindow, EffectStarter effectStarter)
        {
            _starField = starField;
            _progressProvider = progressProvider;
            _applicationStateMachine = applicationStateMachine;
            _levelDataProvider = levelDataProvider;
            _heroShipHolder = heroShipHolder;
            _shipFactory = shipFactory;
            _enemiesHolder = enemiesHolder;
            _bulletHolder = bulletHolder;
            _hudWindow = hudWindow;
            _effectStarter = effectStarter;
        }

        public void Enter()
        {
            LoadProgressPartially();
            CleanUpLevel();
            CreateHero();
            CreateEnemies();
            CreateStars();
            _hudWindow.ResetHud();
            _applicationStateMachine.EnterToState<GameLoopApplicationState>();
        }

        private void LoadProgressPartially()
        {
            int deathCount = _progressProvider.Progress.HeroDeathCount;
            int bumperHitCount = _progressProvider.Progress.BumperHitCount;
            _progressProvider.LoadProgress();
            _progressProvider.Progress.HeroDeathCount = deathCount;
            _progressProvider.Progress.BumperHitCount = bumperHitCount;
        }

        private void CleanUpLevel()
        {
            _enemiesHolder.Clear();
            _heroShipHolder.Clear();
            _bulletHolder.Clear();
            _starField.ClearStars();
            _effectStarter.CleanUp();
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

        private void CreateStars()
        {
            _starField.CreateStars();
        }
    }
}