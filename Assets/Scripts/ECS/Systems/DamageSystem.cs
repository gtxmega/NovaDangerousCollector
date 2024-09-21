using ECS.Components;
using ECS.Components.Attributes;
using ECS.Mark;
using Game.Types;
using Leopotam.Ecs;
using Logics.Displaying;
using UnityEngine;

namespace ECS.Systems
{
    public class DamageSystem : IEcsRunSystem
    {
        private readonly DamageIndicator _damageIndicator;

        private EcsFilter<DamageableComponent> _damageableFilter;

        public DamageSystem(DamageIndicator damageIndicator)
        {
            _damageIndicator = damageIndicator;
        }

        public void Run()
        {
            foreach (var i in _damageableFilter)
            {
                ref var entity = ref _damageableFilter.GetEntity(i);
                ref var damageableComponent = ref _damageableFilter.Get1(i);
                ref var viewComponent = ref entity.Get<ViewComponent>();

                if (damageableComponent.DamageQueue.Count > 0)
                {
                    float generalTotalDamage = 0.0f;

                    ref var healthComponent = ref entity.Get<HealthComponent>();

                    if (entity.Has<ArmorComponent>())
                    {
                        ref var armorComponent = ref entity.Get<ArmorComponent>();

                        for (int j = 0; j < damageableComponent.DamageQueue.Count; ++j)
                        {
                            Damage damage = damageableComponent.DamageQueue.Dequeue();

                            float totalDamage = damage.Amount;

                            if (damage.Instigator.IsAlive() == false)
                                continue;

                            
                            ref var instigatorGeneralAttributes = ref damage.Instigator.Get<GeneralAttributes>();
                            ref var instigatorView = ref damage.Instigator.Get<ViewComponent>();

                            switch (damage.Type)
                            {
                                case EDamageType.PHYSIC:
                                    totalDamage -= totalDamage * armorComponent.PhysicResistance;
                                    break;
                                case EDamageType.MAGIC:
                                    totalDamage -= totalDamage * armorComponent.MagicResistance;
                                    break;
                            }

                            healthComponent.Current -= totalDamage;
                            generalTotalDamage += totalDamage;
                            
                            float vampirism = totalDamage * instigatorGeneralAttributes.Vampirism;

                            if (vampirism > 0)
                            {
                                ref var instigatorHealth = ref damage.Instigator.Get<HealthComponent>();
                                instigatorHealth.Current += vampirism;
                                _damageIndicator.ShowHealthOnDisplay(vampirism, instigatorView.View.SelfTransform);
                            }

                            if (healthComponent.Current < 0.0f)
                            {
                                entity.Get<DiedMark>();
                                break;
                            }
                        }

                    }
                    else
                    {
                        for (int j = 0; j < damageableComponent.DamageQueue.Count; ++j)
                        {
                            Damage damage = damageableComponent.DamageQueue.Dequeue();

                            if (damage.Instigator.IsAlive() == false)
                                continue;

                            healthComponent.Current -= damage.Amount;
                            generalTotalDamage += damage.Amount;

                            if (healthComponent.Current < 0.0f)
                            {
                                entity.Get<DiedMark>();
                                break;
                            }
                        }
                    }

                    if (generalTotalDamage > 0.0f)
                    {

                        _damageIndicator.ShowDamageOnDisplay(generalTotalDamage, viewComponent.View.SelfTransform);
                    }

                }


            }
        }
    }
}