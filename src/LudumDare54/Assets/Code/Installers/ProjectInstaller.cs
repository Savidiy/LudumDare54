using Zenject;

namespace LudumDare54
{
    public sealed class ProjectInstaller : MonoInstaller
    {
        public CameraSettings CameraSettings;
        public HeroSettings HeroSettings;
        public InputSettings InputSettings;
        public ShipStatStaticDataLibrary ShipStatStaticDataLibrary;
        public LevelLibrary LevelLibrary;
        public LevelSettings LevelSettings;
        public ShipStaticDataLibrary ShipStaticDataLibrary;
        public RadarSettings RadarSettings;
        public BulletSettings BulletSettings;
        public HighlightSettings HighlightSettings;
        public ProgressSettings ProgressSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(CameraSettings);
            Container.BindInstance(HeroSettings);
            Container.BindInstance(InputSettings);
            Container.BindInstance(ShipStatStaticDataLibrary);
            Container.BindInstance(LevelLibrary);
            Container.BindInstance(LevelSettings);
            Container.BindInstance(ShipStaticDataLibrary);
            Container.BindInstance(RadarSettings);
            Container.BindInstance(BulletSettings);
            Container.BindInstance(HighlightSettings);
            Container.BindInstance(ProgressSettings);

            Container.Bind<IEventInvoker>().To<UnityEventInvoker>().AsSingle();
            Container.Bind<ProgressProvider>().AsSingle();
        }
    }
}