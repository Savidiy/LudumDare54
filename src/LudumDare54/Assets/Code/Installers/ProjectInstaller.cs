using UnityEngine.Serialization;
using Zenject;

namespace LudumDare54
{
    public sealed class ProjectInstaller : MonoInstaller
    {
        public CameraSettings CameraSettings;
        public HeroSettings HeroSettings;
        public InputSettings InputSettings;
        [FormerlySerializedAs("ShipStaticDataLibrary")] public ShipStatStaticDataLibrary ShipStatStaticDataLibrary;
        public LevelLibrary LevelLibrary;
        public LevelSettings LevelSettings;
        public ShipStaticDataLibrary ShipStaticDataLibrary;
        
        public override void InstallBindings()
        {
            Container.BindInstance(CameraSettings);
            Container.BindInstance(HeroSettings);
            Container.BindInstance(InputSettings);
            Container.BindInstance(ShipStatStaticDataLibrary);
            Container.BindInstance(LevelLibrary);
            Container.BindInstance(LevelSettings);
            Container.BindInstance(ShipStaticDataLibrary);
            
            Container.Bind<IEventInvoker>().To<UnityEventInvoker>().AsSingle();
            Container.Bind<AssetProvider>().AsSingle();
        }
    }
}