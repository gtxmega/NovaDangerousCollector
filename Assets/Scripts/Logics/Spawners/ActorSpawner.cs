using ECS.Components;
using Leopotam.Ecs;
using Services.Events;
using Services.Factory;
using Services.Factory.Builders;
using Services.locator;
using UnityEngine;

namespace Logics.Spawners
{
    public class ActorSpawner : MonoBehaviour, InjectDependency
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private EntityConfig _entityConfig;
        [SerializeField] private WeaponConfig _weaponConfig;
        [SerializeField] private int _spawnCount;
        [SerializeField] private float _spawnRadius;

        private IActorFactory _actorFactory;
        private ILevelEvents _levelEvents;

        public void Inject(IServicesLocator locator)
        {
            _actorFactory = locator.GetServices<IActorFactory>();
            _levelEvents = locator.GetServices<ILevelEvents>();

            _levelEvents.LevelStart += OnLevelStart;
        }

        private void OnLevelStart()
        {
            for (int i = 0; i < _spawnCount; ++i)
            {
                ref var entity = ref _actorFactory.CreateEntity(_entityConfig, GetRandomPositionIn(_spawnRadius));
                ref var actorComponent = ref entity.Get<ActorComponent>();

                Transform freeWeaponSocket = actorComponent.View.GetFreeWeaponSocket();
                if (freeWeaponSocket != null)
                {
                    _actorFactory.CreateWeaponEntity(_weaponConfig, in entity, freeWeaponSocket);
                }
            }
        }

        private Vector3 GetRandomPositionIn(float radius)
        {
            Vector3 randomSpawnPosition = Random.insideUnitSphere * _spawnRadius;
            randomSpawnPosition.y = _spawnPoint.position.y;

            return _spawnPoint.position + randomSpawnPosition;
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
                Gizmos.DrawWireSphere(_spawnPoint.position, _spawnRadius);
            }
        }

#endif
    }
}