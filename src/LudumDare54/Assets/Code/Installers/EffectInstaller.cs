using Zenject;

namespace LudumDare54
{
    public class EffectInstaller : Installer<EffectInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<EffectStarter>().AsSingle();
            Container.Bind<EffectPool>().AsSingle();
            Container.Bind<EffectHolder>().AsSingle();
            Container.Bind<EffectUpdater>().AsSingle();
            Container.Bind<EffectFactory>().AsSingle();
        }
    }
}