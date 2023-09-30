namespace LudumDare54
{
    public interface IShipControls
    {
        float Move { get; }
        float Rotate { get; }
        float Strafe { get; }
    }
}