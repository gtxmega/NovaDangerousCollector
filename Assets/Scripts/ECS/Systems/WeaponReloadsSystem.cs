using ECS.Components;
using ECS.Components.Attributes;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
    public class WeaponReloadsSystem : IEcsRunSystem
    {
        private EcsFilter<WeaponComponent, WeaponAttributesComponent> _weaponFilter;

        public void Run()
        {
            foreach (var i in _weaponFilter)
            {
                ref var weapon = ref _weaponFilter.Get1(i);


                if (weapon.IsReady == false)
                {
                    ref var weaponAttributes = ref _weaponFilter.Get2(i);

                    if (weaponAttributes.ReloadsTimer >= 0.0f)
                    {
                        ref var generalAttributes = ref weapon.Owner.Get<GeneralAttributes>();

                        float reloadsSpeed = generalAttributes.ReloadsSpeed + weaponAttributes.ReloadsSpeed;
                        reloadsSpeed /= 100.0f;

                        weaponAttributes.ReloadsTimer -= (1.0f + reloadsSpeed) * Time.fixedDeltaTime;
                    }
                    else
                    {
                        weapon.IsReady = true;
                    }
                }
            }
        }
    }
}