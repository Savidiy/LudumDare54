using Zenject;

namespace LudumDare54
{
    public sealed class ProjectInstaller : MonoInstaller
    {
        public CameraSettings CameraSettings;
        
        public override void InstallBindings()
        {
            Container.BindInstance(CameraSettings);
            
            Container.Bind<IEventInvoker>().To<UnityEventInvoker>().AsSingle();
            Container.Bind<AssetProvider>().AsSingle();
        }
    }
}