namespace LudumDare54
{
    public class ShipSounds
    {
        public SoundIdData HurtSoundId { get; }
        public SoundIdData ShootSoundId { get; }

        public ShipSounds(SoundIdData shootSoundId, SoundIdData hurtSoundId)
        {
            HurtSoundId = hurtSoundId;
            ShootSoundId = shootSoundId;
        }
    }
}