namespace LudumDare54
{
    public struct PlayerInputData
    {
        public float Move;
        public float Rotate;
        public float Strafe;

        public PlayerInputData(float move, float rotate, float strafe)
        {
            Move = move;
            Rotate = rotate;
            Strafe = strafe;
        }
    }
}