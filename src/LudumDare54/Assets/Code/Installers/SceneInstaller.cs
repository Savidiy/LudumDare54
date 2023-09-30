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

            Container.Bind<LevelDataProvider>().AsSingle();

            Container.Bind<HeroShipHolder>().AsSingle();
            Container.Bind<EnemiesHolder>().AsSingle();
            Container.Bind<ShipFactory>().AsSingle();
            Container.Bind<PlayerInputProvider>().AsSingle();
            Container.Bind<ShipMoveInvoker>().AsSingle();
            

            Container.Bind<HeroCameraTracker>().AsSingle();
            Container.Bind<CameraProvider>().AsSingle();
            Container.Bind<HudSwitcher>().AsSingle();
        }
    }
}