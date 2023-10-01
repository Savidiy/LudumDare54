namespace LudumDare54
{
    public struct HeroInputData
    {
        public readonly float Move;
        public readonly float Rotate;
        public readonly float Strafe;

        public HeroInputData(float move, float rotate, float strafe)
        {
            Move = move;
            Rotate = rotate;
            Strafe = strafe;
        }
    }
}