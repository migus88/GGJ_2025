using System;

namespace Managers
{
    public interface IAirManager
    {
        float CurrentAir { get; }

        void AddAir(float amountOfAir);
        void EmptyAir();
        void StartDeflation();
        void PauseDeflation();
    }
}