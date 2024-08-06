using ECS.Components;
using Leopotam.Ecs;
using Levels;
using Logics.Actors;
using Services.Events;
using Services.Factory;
using Services.locator;
using UnityEngine;

namespace Logics.Spawners
{
    public class PlayerSpawner : MonoBehaviour, InjectDependency
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private PlayerActorConfig _config;

        private IActorFactory _actorFactory;
        private LevelState _levelState;
        private ILevelEvents _levelEvents;
        private ILevelEventsExec _levelEventsExec;

        public void Inject(IServicesLocator locator)
        {
            _actorFactory = locator.GetServices<IActorFactory>();
            _levelState = locator.GetServices<LevelState>();
            _levelEvents = locator.GetServices<ILevelEvents>();
            _levelEventsExec = locator.GetServices<ILevelEventsExec>();

            _levelEvents.LevelStart += OnLevelStart;
        }

        private void OnLevelStart()
        {
            ref var entity = ref _actorFactory.CreateEntity(_config, _spawnPoint.position);
            ref var playerComponent = ref entity.Get<PlayerComponent>();

            _levelState.TryConfigurePlayerEntity(playerComponent.View);
            _levelEventsExec.OnPlayerSpawnActor(playerComponent.View);
        }

        private void OnDestroy()
        {
            if (_levelEvents != null)
            {
                _levelEvents.LevelStart -= OnLevelStart;
            }
        }

#if UNITY_EDITOR

        [SerializeField] private Color _color = Color.white;
        [SerializeField] private bool _isDisplaying = true;

        private void OnDrawGizmos()
        {
            if (_isDisplaying && _spawnPoint != null)
            {
                Gizmos.color = _color;
                Gizmos.DrawCube(_spawnPoint.position, Vector3.one);
            }
        }

#endif
    }
}