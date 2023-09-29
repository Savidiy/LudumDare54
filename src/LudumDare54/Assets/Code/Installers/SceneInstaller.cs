using TMPro;
using Zenject;

namespace LudumDare54
{
    public sealed class SceneInstaller : MonoInstaller
    {
        public TMP_Text Text;

        public override void InstallBindings()
        {
            Container.BindInstance(Text);
            Container.Bind<TextUpdater>().AsSingle();
            
            Container.Bind<ApplicationStateMachine>().AsSingle();
            Container.Bind<IApplicationState>().To<GameLoopApplicationState>().AsSingle();
        }
    }
}