using ECS.Components;
using ECS.Mark;
using Leopotam.Ecs;
using Services.Events;
using UnityEngine;

namespace ECS.Systems
{
    public class DestroyingSystem : IEcsRunSystem
    {
        private EcsFilter<DiedMark> _diedFilter;

        private readonly ILevelEventsExec _levelEventsExec;

        public DestroyingSystem(ILevelEventsExec levelEventsExec)
        {
            _levelEventsExec = levelEventsExec;
        }

        public void Run()
        {
            foreach (var i in _diedFilter)
            {
                ref var entity = ref _diedFilter.GetEntity(i);

                if (entity.Has<PlayerComponent>())
                {
                    _levelEventsExec.OnPlayerDie();
                }else if(entity.Has<ActorComponent>())
                {
                    _levelEventsExec.OnActorDie();
                }

                if (entity.Has<ViewComponent>())
                {
                    ref var viewComponent = ref entity.Get<ViewComponent>();
                    Object.Destroy(viewComponent.View.gameObject);
                }

                if (entity.Has<ProjectileComponent>())
                {
                    ref var projectileComponent = ref entity.Get<ProjectileComponent>();
                    Object.Destroy(projectileComponent.View.gameObject);
                }

                if (entity.Has<WeaponComponent>())
                {
                    ref var weaponComponent = ref entity.Get<WeaponComponent>();
                    Object.Destroy(weaponComponent.View.gameObject);
                }

                entity.Destroy();
            }
        }
    }
}