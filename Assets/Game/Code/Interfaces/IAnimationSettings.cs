namespace Game.Code.Interfaces
{
    public interface IAnimationSettings
    {
        string Walk { get; }
        string Idle { get; }
        string Fall { get; }
        string Inflate { get; }
        string Fly { get; }
        string Jump { get; }
        string Healthy { get; }
        string Hurt { get; }
        string AlmostDead { get; }
        string Death { get; }
        string MidAirDeath { get; }
        string Spawn { get; }
    }
}