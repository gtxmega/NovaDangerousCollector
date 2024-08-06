using ECS.Components.Attributes;
using ECS.Mark;
using Leopotam.Ecs;
using Services.Factory.Builders;

namespace Logics.Actors
{
    public class NpcActorBuilder : EntityBuilder
    {
        private readonly NpcActorConfig _config;

        public NpcActorBuilder(NpcActorConfig config) : base(config)
        {
            _config = config;
        }

        public override void Make()
        {
            base.Make();

            ref var generalAttributes = ref _entity.Get<GeneralAttributes>();
            generalAttributes.Damage = _config.Damage;
            generalAttributes.Vampirism = _config.Vampirism;
            generalAttributes.CriticalChance = _config.CriticalChance;
            generalAttributes.CriticalDamage = _config.CriticalDamage;
            generalAttributes.ReloadsSpeed = _config.AttackReloadsSpeed;
            generalAttributes.Armor = _config.Armor;

            ref var armorComponent = ref _entity.Get<ArmorComponent>();
            armorComponent.PhysicResistance = _config.BasePhysicResistance;
            armorComponent.MagicResistance = _config.BaseMagicResistance;

            _entity.Get<SpawnMark>();
        }
    }
}