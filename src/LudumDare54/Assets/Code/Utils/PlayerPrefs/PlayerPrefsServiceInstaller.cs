using Zenject;

namespace Savidiy.Utils
{
    public sealed class PlayerPrefsServiceInstaller : Installer<PlayerPrefsServiceInstaller>
    {
        public override void InstallBindings()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            Container.Bind<IPlayerPrefsService>().To<WebGLPlayerPrefsService>().AsSingle();
#else
            Container.Bind<IPlayerPrefsService>().To<DefaultPlayerPrefsService>().AsSingle();
#endif
        }
    }
}