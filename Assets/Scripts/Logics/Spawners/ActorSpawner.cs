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
            ref var entity = ref _actorFactory.CreateEntity(_entityConfig, _spawnPoint.position);
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