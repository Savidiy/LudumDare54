using Zenject;

namespace LudumDare54
{
    public sealed class ProjectInstaller : MonoInstaller
    {
        public CameraSettings CameraSettings;
        public HeroSettings HeroSettings;
        public InputSettings InputSettings;
        public ShipStaticDataLibrary ShipStaticDataLibrary;
        
        public override void InstallBindings()
        {
            Container.BindInstance(CameraSettings);
            Container.BindInstance(HeroSettings);
            Container.BindInstance(InputSettings);
            Container.BindInstance(ShipStaticDataLibrary);
            
            Container.Bind<IEventInvoker>().To<UnityEventInvoker>().AsSingle();
            Container.Bind<AssetProvider>().AsSingle();
        }
    }
}