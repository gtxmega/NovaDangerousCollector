using ECS.Components;
using ECS.Components.Attributes;
using Leopotam.Ecs;

namespace ECS.Systems
{
    public class EnemyMovementSystem : IEcsRunSystem
    {
        private EcsFilter<OrdersComponent, MovementComponent> _filters;

        public void Run()
        {
            foreach (var i in _filters)
            {
                ref var entity = ref _filters.GetEntity(i);

                ref var orders = ref _filters.Get1(i);
                ref var movement = ref _filters.Get2(i);
                ref var entityView = ref _filters.GetEntity(i);

                if(orders.NearTarget != null)
                {

                }

            }
        }
    }
}