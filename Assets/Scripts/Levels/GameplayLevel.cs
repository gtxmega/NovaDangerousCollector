using ECS.Mark;
using ECS.Systems;
using ECS.Systems.Indicators;
using ECS.Systems.Initialize;
using Game;
using Leopotam.Ecs;
using Logics.Displaying;
using Services.Events;
using Services.Factory;
using Services.locator;
using UnityEngine;
using Utility;

namespace Levels
{
    public class GameplayLevel : LevelInstance
    {
        [SerializeField] private GameplayServices _services;

        private EcsWorld _world;
        private EcsSystems _fixedUpdateSystems;

        private IServicesLocator _locator;


        public override void Init(GameInstance gameInstance)
        {
            _world = new EcsWorld();

            _services.Init(gameInstance, _world);
            _services.InjectToWorldObject();
            _locator = _services.GetLocator();

            LevelLoaded();

            CreateSystems();

            StartLevel();
        }

        public override void LevelLoaded()
        {
            _locator.GetServices<ILevelEventsExec>().OnLevelLoaded();
        }

        public override void StartLevel()
        {
            _locator.GetServices<ILevelEventsExec>().OnLevelStart();
        }

        private void FixedUpdate()
        {
            _fixedUpdateSystems?.Run();
        }

        private void CreateSystems()
        {
            _fixedUpdateSystems = new EcsSystems(_world);

            _fixedUpdateSystems
                .Add(new InitializeArtifacSystem(_locator.GetServices<ILevelEventsExec>()))
                .Add(new SearchEnemySystem(_locator.GetServices<GameConstants>()))
                .Add(new WeaponLookToTargetSystem())
                .Add(new ShootingSystem(_locator.GetServices<IActorFactory>()))
                .Add(new ProjectileMovementSystem())
                .Add(new WeaponReloadsSystem())
                .Add(new HealthRegenerationSystem())
                .Add(new DamageSystem(_locator.GetServices<DamageIndicator>()))
                .Add(new HealthIndicatorSystem(_locator.GetServices<CoordinatesUtility>(), _locator.GetServices<HealthIndicatorManager>()))
                .Add(new ArtifactsCooldownSystem())
                .Add(new DiademOfMadnessSystem())
                .Add(new WeaponDestroySystem())
                .Add(new ArtifactsDestroySystem())
                .Add(new DestroyingSystem());

            _fixedUpdateSystems
                .OneFrame<SpawnMark>();

            _fixedUpdateSystems.Init();
        }
    }
}