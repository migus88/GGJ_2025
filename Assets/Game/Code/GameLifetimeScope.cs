using Game.Code.Interfaces;
using Game.Code.Player;
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
        [SerializeField] private AnimationSettings _animationSettings;

        [SerializeField] private PlayerController _playerController;
        [SerializeField] private SoundManager _soundManager;
        [SerializeField] private Exit _exit;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance<IGameSettings>(_gameSettings);
            builder.RegisterInstance<IControlsSettings>(_controlsSettings);
            builder.RegisterInstance<ISoundSettings>(_soundSettings);
            builder.RegisterInstance<IAnimationSettings>(_animationSettings);
            builder.RegisterInstance<IExit>(_exit);
            builder.RegisterInstance<ISoundManager>(_soundManager);
            builder.RegisterInstance(_playerController);

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