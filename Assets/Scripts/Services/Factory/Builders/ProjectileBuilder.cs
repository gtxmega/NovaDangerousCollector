using ECS.Components;
using Leopotam.Ecs;
using Logics.Views;
using UnityEngine;

namespace Services.Factory.Builders
{
    public class ProjectileBuilder
    {
        protected EcsEntity _entity;
        protected ProjectileView _projectileView;
        protected EcsWorld _world;

        protected Vector3 _spawnLocation;
        private Quaternion _spawnRotation;
        private EcsEntity _owner;

        private readonly ProjectileConfig _config;

        public ProjectileBuilder(ProjectileConfig config) => _config = config;

        public void SetLocation(Vector3 spawnLocation) => _spawnLocation = spawnLocation;
        public void SetRotation(Quaternion spawnRotation) => _spawnRotation = spawnRotation;
        public void SetWorld(EcsWorld world) => _world = world;
        public void SetOwner(in EcsEntity owner) => _owner = owner;

        public virtual void Make()
        {
            _entity = _world.NewEntity();

            _projectileView = Object.Instantiate(_config.ProjectilePrefab, _spawnLocation, _spawnRotation);
            _projectileView.Init(_entity, _world);

            ref var projectileComponent = ref _entity.Get<ProjectileComponent>();
            projectileComponent.Owner = _owner;
            projectileComponent.View = _projectileView;
            projectileComponent.Speed = _config.Speed;

            ref var ownerViewComponent = ref _owner.Get<ViewComponent>();
            ref var ownerUnionsComponent = ref _owner.Get<UnionsComponent>();
            ref var unionsComponent = ref _entity.Get<UnionsComponent>();
            unionsComponent.EnemyTeams = ownerUnionsComponent.EnemyTeams;
            unionsComponent.EnemyLayers = ownerUnionsComponent.EnemyLayers;

            _projectileView.SetTeam(ownerViewComponent.View.Team);

        }

        public ProjectileView GetView() => _projectileView;
        public ref EcsEntity GetResult() => ref _entity;
    }
}