using ECS.Components;
using ECS.Components.Attributes;
using Game.Types;
using Leopotam.Ecs;
using Services.Factory;
using UnityEngine;

namespace ECS.Systems
{
    public class ShootingSystem : IEcsRunSystem
    {
        private EcsFilter<WeaponComponent, WeaponAttributesComponent> _weaponFilter;

        private readonly IActorFactory _actorFactory;

        public ShootingSystem(IActorFactory actorFactory)
        {
            _actorFactory = actorFactory;
        }

        public void Run()
        {
            foreach (var i in _weaponFilter)
            {
                ref var weapon = ref _weaponFilter.Get1(i);


/*                if (weapon.TargetActor != null)
                {
                    weapon.View.TargetLine.SetPosition(0, weapon.View.ProjectileSocket.position);
                    weapon.View.TargetLine.SetPosition(1, weapon.TargetActor.SelfTransform.position);
                }
                else
                {
                    weapon.View.TargetLine.SetPosition(0, weapon.View.ProjectileSocket.position);
                    weapon.View.TargetLine.SetPosition(1, weapon.View.ProjectileSocket.position);
                }*/

                if (weapon.IsReady && weapon.TargetActor != null)
                {
                    ref var weaponAttributes = ref _weaponFilter.Get2(i);
                    ref var generalAttributes = ref weapon.Owner.Get<GeneralAttributes>();

                    float criticalHit = Random.Range(0.0f, 100.0f);
                    float generalCriticalChance = generalAttributes.CriticalChance + weaponAttributes.CriticalChance;
                    float criticalDamage = generalAttributes.CriticalDamage + weaponAttributes.CriticalDamage;


                    if (criticalHit <= generalCriticalChance)
                    {
                        ref EcsEntity projectileEntity = ref _actorFactory.CreateProjectileEntity(weapon.CriticalProjectileConfig,
                                                                                    weapon.Owner, weapon.ProjectileSocket);

                        ref var projectile = ref projectileEntity.Get<ProjectileComponent>();

                        projectile.Damage = new Damage()
                        {
                            Amount = (weaponAttributes.Damage + generalAttributes.Damage) * criticalDamage,
                            Instigator = weapon.Owner,
                            Type = weaponAttributes.DamageType
                        };

                        projectile.Target = weapon.TargetActor;

                        switch (weapon.AttackType)
                        {
                            case EAttackType.NONTARGET:

                                Vector3 targetPosition = weapon.TargetActor.SelfTransform.position;
                                Vector3 direction = targetPosition - weapon.View.ProjectileSocket.position;
                                direction.Normalize();

                                projectile.IsTargetMove = false;
                                projectile.EndPosition = targetPosition + direction * (weaponAttributes.RadiusOrder / 2);

                                break;
                            case EAttackType.TARGET:

                                projectile.IsTargetMove = true;

                                break;
                            case EAttackType.MELEE:

                                break;
                        }
                    }
                    else
                    {
                        ref EcsEntity projectileEntity = ref _actorFactory.CreateProjectileEntity(weapon.ProjectileConfig,
                                                                                    weapon.Owner, weapon.ProjectileSocket);

                        ref var projectile = ref projectileEntity.Get<ProjectileComponent>();

                        projectile.Damage = new Damage()
                        {
                            Amount = weaponAttributes.Damage + generalAttributes.Damage,
                            Instigator = weapon.Owner,
                            Type = weaponAttributes.DamageType
                        };

                        projectile.Target = weapon.TargetActor;

                        switch (weapon.AttackType)
                        {
                            case EAttackType.NONTARGET:

                                Vector3 targetPosition = weapon.TargetActor.SelfTransform.position;
                                Vector3 direction = targetPosition - weapon.View.ProjectileSocket.position;
                                direction.Normalize();

                                projectile.IsTargetMove = false;
                                projectile.EndPosition = targetPosition + direction * (weaponAttributes.RadiusOrder / 2);

                                break;
                            case EAttackType.TARGET:

                                projectile.IsTargetMove = true;

                                break;
                            case EAttackType.MELEE:

                                break;
                        }

                    }

                    weapon.IsReady = false;
                    weaponAttributes.ReloadsTimer = weaponAttributes.Reloads;
                }
            }
        }
    }
}