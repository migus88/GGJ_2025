using Game.Code.Interfaces;
using Managers;
using ScriptableObjects;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Code
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private ControlsSettings _controlsSettings;
        [SerializeField] private SoundSettings _soundSettings;

        [SerializeField] private SoundManager _soundManager;
        [SerializeField] private Exit _exit;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance<IGameSettings>(_gameSettings);
            builder.RegisterInstance<IControlsSettings>(_controlsSettings);
            builder.RegisterInstance<ISoundSettings>(_soundSettings);
            
            builder.RegisterComponent<IExit>(_exit);
            builder.RegisterComponent<ISoundManager>(_soundManager);

            builder.Register<IAirManager, AirManager>(Lifetime.Singleton);
            
            builder.UseEntryPoints(c =>
            {
                c.Add<DistanceManager>().As<IDistanceManager>();
                c.Add<TimeService>().As<ITimeService>();
                c.Add<InputService>().As<IInputService>();
            });
        }
    }
}