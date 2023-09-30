namespace LudumDare54
{
    public interface IShipStats
    {
        float RotationSpeed { get; }
        float ForwardSpeed { get; }
        float BackwardSpeed { get; }
        float StrafeSpeed { get;  }
    }
}