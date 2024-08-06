using Game;
using Leopotam.Ecs;
using Logics.Displaying;
using Services.Events;
using Services.Factory;
using Services.InputHandler;
using Services.locator;
using UnityEngine;
using Utility;

namespace Levels
{
    public class GameplayServices : MonoBehaviour
    {
        [SerializeField] private DamageIndicator _damageIndicator;
        [SerializeField] private LevelRewards _levelRewards;
        [SerializeField] private MobileInput _mobileInput;
        [SerializeField] private HealthIndicatorManager _healthIndicatorManager;
        [SerializeField] private CoordinatesUtility _coordinatesUtility;

        private ServicesLocator _locator;

        private LevelEvents _levelEvents;
        private ArtifactCastEventsUI _artifactsCastEventsUI;
        private ActorFactory _actorFactory;

        public void Init(GameInstance gameInstance, EcsWorld world)
        {
            _locator = new();

            _levelEvents = new LevelEvents();
            _artifactsCastEventsUI = new ArtifactCastEventsUI();
            _actorFactory = new ActorFactory(world, _locator);

            _locator
                .Registration<GameInstance>(gameInstance)
                .Registration<LevelState>(gameInstance.LevelState)
                .Registration<GameConstants>(gameInstance.GameConstants)
                .Registration<ILevelEvents>(_levelEvents)
                .Registration<ILevelEventsExec>(_levelEvents)
                .Registration<IArtifactCastEventsUI>(_artifactsCastEventsUI)
                .Registration<IArtifactCastEventsUIExec>(_artifactsCastEventsUI)
                .Registration<IActorFactory>(_actorFactory)
                .Registration<IInputHandler>(_mobileInput)
                .Registration<HealthIndicatorManager>(_healthIndicatorManager)
                .Registration<CoordinatesUtility>(_coordinatesUtility)
                .Registration<LevelRewards>(_levelRewards)
                .Registration<DamageIndicator>(_damageIndicator);
        }

        public void InjectToWorldObject()
        {
            foreach (var mono in FindObjectsOfType<MonoBehaviour>())
            {
                if (mono is InjectDependency target)
                    target.Inject(_locator);
            }
        }

        public IServicesLocator GetLocator() => _locator;
    }
}