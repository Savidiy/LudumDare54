namespace LudumDare54
{
    public static class TestEffectBridge
    {
        private static EffectStarter _effectStarter;

        public static void TestEffect(EffectType effectType)
        {
            _effectStarter.TestEffect(effectType);            
        }

        public static void Register(EffectStarter effectStarter)
        {
            _effectStarter = effectStarter;
        }

        public static void Unregister()
        {
            _effectStarter = null;
        }
    }
}