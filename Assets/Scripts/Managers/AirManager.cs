using System;

namespace Managers
{
    public class AirManager : IAirManager
    {
        public float CurrentAir { get; private set; }
        public event Action AirDepleted;

        private float _reductionSpeed = 0.1f;
        private bool _isDeflating = false;

        public AirManager(TimeService timeService)
        {
            timeService.SecondPassed += HandleDeflation;
        }
        public void AddAir(float amountOfAir)
        {
            CurrentAir += amountOfAir;
        }

        public void EmptyAir()
        {
            _reductionSpeed = 0.5f;
        }

        public void StartDeflation()
        {
            _isDeflating = true;
        }

        public void PauseDeflation()
        {
            _isDeflating = false;
        }

        private void HandleDeflation()
        {
            if (CurrentAir <= 0f)
            {
                CurrentAir = 0f;
                _isDeflating = false;
                AirDepleted?.Invoke();
                return;
            }

            CurrentAir -= _reductionSpeed;
        }
    }
}
