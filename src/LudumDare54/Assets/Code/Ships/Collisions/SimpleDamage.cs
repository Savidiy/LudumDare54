namespace LudumDare54
{
    public sealed class SimpleDamage : IShipDamage
    {
        public int Damage { get; }

        public SimpleDamage(int damage)
        {
            Damage = damage;
        }
    }
}