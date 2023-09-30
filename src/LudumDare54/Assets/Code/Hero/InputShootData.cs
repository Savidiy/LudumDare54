namespace LudumDare54
{
    public struct InputShootData
    {
        public readonly bool HasFire1;
        public readonly bool HasFire2;
        public readonly bool HasFire3;
        public readonly bool HasFire4;

        public InputShootData(bool hasFire1, bool hasFire2, bool hasFire3, bool hasFire4)
        {
            HasFire1 = hasFire1;
            HasFire2 = hasFire2;
            HasFire3 = hasFire3;
            HasFire4 = hasFire4;
        }
    }
}