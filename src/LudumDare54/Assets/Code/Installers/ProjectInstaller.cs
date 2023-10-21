using Sirenix.OdinInspector;
using Zenject;

namespace LudumDare54
{
    public sealed class ProjectInstaller : MonoInstaller
    {
        [Required] public CameraSettings CameraSettings;
        [Required] public HeroSettings HeroSettings;
        [Required] public InputSettings InputSettings;
        [Required] public LevelLibrary LevelLibrary;
        [Required] public LevelSettings LevelSettings;
        [Required] public RadarSettings RadarSettings;
        [Required] public BulletLibrary BulletLibrary;
        [Required] public HighlightSettings HighlightSettings;
        [Required] public ProgressSettings ProgressSettings;
        [Required] public AsteroidLibrary AsteroidLibrary;
        [Required] public TurretLibrary TurretLibrary;
        [Required] public StarFieldSettings StarFieldSettings;
        [Required] public GravyLibrary GravyLibrary;
        [Required] public SoundLibrary SoundLibrary;
        [Required] public SoundSettings SoundSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(CameraSettings);
            Container.BindInstance(HeroSettings);
            Container.BindInstance(InputSettings);
            Container.BindInstance(LevelLibrary);
            Container.BindInstance(LevelSettings);
            Container.BindInstance(RadarSettings);
            Container.BindInstance(BulletLibrary);
            Container.BindInstance(HighlightSettings);
            Container.BindInstance(ProgressSettings);
            Container.BindInstance(AsteroidLibrary);
            Container.BindInstance(TurretLibrary);
            Container.BindInstance(StarFieldSettings);
            Container.BindInstance(GravyLibrary);
            Container.BindInstance(SoundLibrary);
            Container.BindInstance(SoundSettings);

            Container.Bind<IEventInvoker>().To<UnityEventInvoker>().AsSingle();
            Container.Bind<ProgressProvider>().AsSingle();
        }
    }
}