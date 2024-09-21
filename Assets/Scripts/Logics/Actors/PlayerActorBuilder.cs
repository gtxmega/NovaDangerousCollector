using ECS.Components;
using ECS.Components.Attributes;
using ECS.Mark;
using Leopotam.Ecs;
using Logics.Views;
using Services.Factory.Builders;

namespace Logics.Actors
{
    public class PlayerActorBuilder : EntityBuilder
    {
        private readonly PlayerActorConfig _config;

        public PlayerActorBuilder(PlayerActorConfig config) : base(config)
        {
            _config = config;
        }

        public override void Make()
        {
            base.Make();

            ref var playerComponent = ref _entity.Get<PlayerComponent>();
            playerComponent.View = _view as PlayerView;

            ref var generalAttributes = ref _entity.Get<GeneralAttributes>();
            generalAttributes.Damage = _config.Damage;
            generalAttributes.Vampirism = _config.Vampirism;
            generalAttributes.CriticalChance = _config.CriticalChance;
            generalAttributes.CriticalDamage = _config.CriticalDamage;
            generalAttributes.ReloadsSpeed = _config.AttackInterval;
            generalAttributes.Armor = _config.Armor;

            _entity.Get<SpawnMark>();
        }
    }
}