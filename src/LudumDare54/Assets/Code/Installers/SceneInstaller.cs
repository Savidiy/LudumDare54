using Zenject;

namespace LudumDare54
{
    public sealed class SceneInstaller : MonoInstaller
    {
        public HudBehaviour HudBehaviour;
        public WinLevelBehaviour WinLevelBehaviour;
        public MainMenuBehaviour MainMenuBehaviour;
        public LoseLevelBehaviour LoseLevelBehaviour;
        public WinGameBehaviour WinGameBehaviour;

        public override void InstallBindings()
        {
            Container.BindInstance(HudBehaviour);
            Container.BindInstance(WinLevelBehaviour);
            Container.BindInstance(MainMenuBehaviour);
            Container.BindInstance(LoseLevelBehaviour);
            Container.BindInstance(WinGameBehaviour);

            Container.Bind<ApplicationStateMachine>().AsSingle();
            Container.BindInterfacesTo<MainMenuApplicationState>().AsSingle();
            Container.BindInterfacesTo<LoadingLevelApplicationState>().AsSingle();
            Container.BindInterfacesTo<GameLoopApplicationState>().AsSingle();
            Container.BindInterfacesTo<WinLevelApplicationState>().AsSingle();
            Container.BindInterfacesTo<LoseLevelApplicationState>().AsSingle();
            Container.BindInterfacesTo<WinGameApplicationState>().AsSingle();

            Container.Bind<LevelDataProvider>().AsSingle();
            
            Container.Bind<ShipFactory>().AsSingle();
            Container.BindInterfacesTo<HeroShipFactory>().AsSingle();
            Container.BindInterfacesTo<AsteroidFactory>().AsSingle();
            Container.BindInterfacesTo<TurretFactory>().AsSingle();

            Container.Bind<HeroShipHolder>().AsSingle();
            Container.Bind<EnemiesHolder>().AsSingle();
            Container.Bind<InputProvider>().AsSingle();
            Container.Bind<ShipMoveInvoker>().AsSingle();
            Container.Bind<ShipShootInvoker>().AsSingle();
            Container.Bind<Radar>().AsSingle();
            Container.Bind<LimitedSpaceChecker>().AsSingle();
            Container.Bind<ShipDeathChecker>().AsSingle();
            Container.Bind<ShipHealthTicker>().AsSingle();
            Container.Bind<ShipCollisionChecker>().AsSingle();

            Container.Bind<BulletHolder>().AsSingle();
            Container.Bind<BulletFactory>().AsSingle();
            Container.Bind<BulletMoveInvoker>().AsSingle();
            Container.Bind<BulletCleaner>().AsSingle();
            Container.Bind<BulletCollisionChecker>().AsSingle();
            Container.Bind<BulletLifeTimeUpdater>().AsSingle();

            Container.Bind<HeroCameraTracker>().AsSingle();
            Container.Bind<CameraProvider>().AsSingle();
            Container.Bind<HudWindow>().AsSingle();

            Container.Bind<WinLoseChecker>().AsSingle();
            Container.Bind<WinLevelWindow>().AsSingle();
            Container.Bind<WinGameWindow>().AsSingle();
            Container.Bind<LoseLevelWindow>().AsSingle();
            Container.Bind<MainMenuWindow>().AsSingle();
        }
    }
}