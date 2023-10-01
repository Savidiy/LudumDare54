using Zenject;

namespace LudumDare54
{
    public sealed class SceneInstaller : MonoInstaller
    {
        public HudBehaviour HudBehaviour;

        public override void InstallBindings()
        {
            Container.BindInstance(HudBehaviour);
            Container.Bind<ApplicationStateMachine>().AsSingle();
            Container.BindInterfacesTo<LoadingLevelApplicationState>().AsSingle();
            Container.BindInterfacesTo<GameLoopApplicationState>().AsSingle();
            Container.BindInterfacesTo<UnloadingLevelApplicationState>().AsSingle();

            Container.Bind<LevelDataProvider>().AsSingle();

            Container.Bind<HeroShipHolder>().AsSingle();
            Container.Bind<EnemiesHolder>().AsSingle();
            Container.Bind<ShipFactory>().AsSingle();
            Container.Bind<InputProvider>().AsSingle();
            Container.Bind<ShipMoveInvoker>().AsSingle();
            Container.Bind<ShipShootInvoker>().AsSingle();
            Container.Bind<Radar>().AsSingle();
            Container.Bind<LimitedSpaceChecker>().AsSingle();
            Container.Bind<ShipDeathChecker>().AsSingle();
            
            Container.Bind<BulletHolder>().AsSingle();
            Container.Bind<BulletFactory>().AsSingle();
            Container.Bind<BulletMoveInvoker>().AsSingle();
            Container.Bind<BulletCleaner>().AsSingle();
            Container.Bind<BulletCollisionChecker>().AsSingle();
            Container.Bind<BulletLifeTimeUpdater>().AsSingle();

            Container.Bind<HeroCameraTracker>().AsSingle();
            Container.Bind<CameraProvider>().AsSingle();
            Container.Bind<HudSwitcher>().AsSingle();
        }
    }
}