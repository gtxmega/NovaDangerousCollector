using ECS.Components;
using ECS.Mark;
using Leopotam.Ecs;

namespace ECS.Systems
{
    public class WeaponDestroySystem : IEcsRunSystem
    {
        private EcsFilter<WeaponComponent> _weaponFilter;

        public void Run()
        {
            foreach (var idx in _weaponFilter)
            {
                ref var weaponFilter = ref _weaponFilter.Get1(idx);

                if(weaponFilter.Owner.Has<DiedMark>())
                {
                    ref var weaponEntity = ref _weaponFilter.GetEntity(idx);
                    weaponEntity.Get<DiedMark>();
                }
            }
        }
    }
}