using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
    public class WeaponLookToTargetSystem : IEcsRunSystem
    {
        private EcsFilter<WeaponComponent> _weaponFilter;

        public void Run()
        {
            foreach (var i in _weaponFilter)
            {
                ref var weaponComponent = ref _weaponFilter.Get1(i);

                if (weaponComponent.TargetActor != null)
                {
                    Vector3 targetPosition = weaponComponent.TargetActor.SelfTransform.position;
                    weaponComponent.View.SelfTransform.LookAt(targetPosition);
                }
            }
        }
    }
}