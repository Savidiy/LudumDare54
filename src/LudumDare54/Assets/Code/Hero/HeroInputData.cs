namespace LudumDare54
{
    public struct HeroInputData
    {
        public float Move;
        public float Rotate;
        public float Strafe;

        public HeroInputData(float move, float rotate, float strafe)
        {
            Move = move;
            Rotate = rotate;
            Strafe = strafe;
        }
    }
}