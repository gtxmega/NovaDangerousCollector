using ECS.Components;
using Leopotam.Ecs;
using Logics.Views;
using UnityEngine;

namespace Services.Factory.Builders
{
    public class EntityBuilder
    {
        protected EcsEntity _entity;
        protected ActorView _view;
        protected EcsWorld _world;

        private Vector3 _spawnLocation;

        private readonly EntityConfig _config;

        public EntityBuilder(EntityConfig config) => _config = config;

        public void SetWorld(EcsWorld world) => _world = world;
        public void SetLocation(Vector3 location) => _spawnLocation = location;

        public virtual void Make()
        {
            _entity = _world.NewEntity();
            _view = Object.Instantiate(_config.ActorView, _spawnLocation, Quaternion.identity);

            ref var healthComponent = ref _entity.Get<HealthComponent>();
            healthComponent.Max = _config.MaxHealth;
            healthComponent.Current = _config.CurrentHealth;
            healthComponent.Regeneration = _config.RegenerationHealth;

            ref var viewComponent = ref _entity.Get<ViewComponent>();
            viewComponent.View = _view;
            viewComponent.HealthWidgetOffset = _config.WidgetOffset;

            ref var damageableComponent = ref _entity.Get<DamageableComponent>();
            damageableComponent.DamageQueue = new();

            ref var unionsComponent = ref _entity.Get<UnionsComponent>();
            unionsComponent.EnemyTeams = _config.EnemyTeams;
            unionsComponent.EnemyLayers = _config.EnemyLayers;

            _view.Init(_entity, _world);
            _view.SetTeam(_config.Team);
        }

        public ActorView GetView() => _view;

        public ref EcsEntity GetResult() => ref _entity;
    }
}