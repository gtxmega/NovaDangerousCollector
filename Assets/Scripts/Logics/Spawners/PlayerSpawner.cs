using ECS.Components;
using ECS.Components.Artifacts;
using Game;
using Leopotam.Ecs;
using Levels;
using Logics.Actors;
using Logics.Artifacts;
using Services.Events;
using Services.Factory;
using Services.Factory.Builders;
using Services.locator;
using UnityEngine;

namespace Logics.Spawners
{
    public class PlayerSpawner : MonoBehaviour, InjectDependency
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private PlayerActorConfig _config;

        private AccoutrementsPlayer _accoutrementsPlayer;
        private IActorFactory _actorFactory;
        private ILevelEvents _levelEvents;
        private ILevelEventsExec _levelEventsExec;

        public void Inject(IServicesLocator locator)
        {
            _accoutrementsPlayer = locator.GetServices<AccoutrementsPlayer>();
            _actorFactory = locator.GetServices<IActorFactory>();
            _levelEvents = locator.GetServices<ILevelEvents>();
            _levelEventsExec = locator.GetServices<ILevelEventsExec>();

            _levelEvents.LevelStart += OnLevelStart;
        }

        private void OnLevelStart()
        {
            ref var entity = ref _actorFactory.CreateEntity(_config, _spawnPoint.position);
            ref var playerComponent = ref entity.Get<PlayerComponent>();

            WeaponConfig[] weapons = _accoutrementsPlayer.GetWeaponConfigs();
            if (weapons != null)
            {
                for (int i = 0; i < weapons.Length; ++i)
                {
                    Transform weaponSocket = playerComponent.View.GetFreeWeaponSocket();
                    if (weaponSocket != null)
                    {
                        _actorFactory.CreateWeaponEntity(weapons[i], in entity, weaponSocket);
                    }
                }
            }

            ArtifactConfig[] artifacts = _accoutrementsPlayer.GetArtifactConfigs();
            if (artifacts != null)
            {
                for (int i = 0; i < artifacts.Length; ++i)
                {
                    ref var artifactEntity = ref _actorFactory.CreateArtifactEntity(artifacts[i], in entity);
                }
            }

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