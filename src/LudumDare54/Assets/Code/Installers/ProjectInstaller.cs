using Zenject;

namespace LudumDare54
{
    public sealed class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IEventInvoker>().To<UnityEventInvoker>().AsSingle();
        }
    }
}