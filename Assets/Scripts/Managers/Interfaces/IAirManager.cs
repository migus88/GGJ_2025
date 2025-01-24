namespace Managers
{
    public interface IAirManager
    {
        float CurrentAir { get; }

        void AddAir();
        void EmptyAir();
        void StartDeflation();
        void PauseDeflation();
    }
}