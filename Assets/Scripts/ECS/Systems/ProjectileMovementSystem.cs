using ECS.Components;
using ECS.Mark;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
    public class ProjectileMovementSystem : IEcsRunSystem
    {
        private EcsFilter<ProjectileComponent> _projectileFilter;

        public void Run()
        {
            foreach (var i in _projectileFilter)
            {
                ref var projectileEntity = ref _projectileFilter.GetEntity(i);
                ref var projectile = ref _projectileFilter.Get1(i);

                Vector3 normalizeDirection = Vector3.zero;
                Vector3 projectilePosition = projectile.View.SelfTransform.position;

                if (projectile.IsTargetMove)
                {
                    if (projectile.Target == null)
                    {
                        projectileEntity.Get<DiedMark>();
                        continue;
                    }

                    Vector3 targetPosition = projectile.Target.SelfTransform.position;
                    Vector3 directionToTarget = targetPosition - projectilePosition;
                    float distance = directionToTarget.magnitude;

                    if (distance <= 0.1f)
                    {
                        projectile.Target.ApplyDamage(projectile.Damage);
                        projectileEntity.Get<DiedMark>();
                    }
                    else
                    {
                        normalizeDirection = directionToTarget / distance;
                    }
                }
                else
                {
                    Vector3 directionToTarget = projectile.EndPosition - projectilePosition;
                    float distanceToTarget = directionToTarget.magnitude;

                    if (distanceToTarget <= 0.1f)
                    {
                        projectileEntity.Get<DiedMark>();
                    }

                    normalizeDirection = directionToTarget / distanceToTarget;
                }

                projectile.View.SelfTransform.position += normalizeDirection * projectile.Speed * Time.fixedDeltaTime;

            }
        }
    }
}