using ECS.Components;
using ECS.Components.Attributes;
using Game;
using Leopotam.Ecs;
using Logics.Views;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace ECS.Systems
{
    public class RadiusRecieveOrdersSystem : IEcsRunSystem
    {
        private EcsFilter<OrdersComponent, MovementComponent> _ordersFilter;

        private Collider[] _enemyBuffer;

        public RadiusRecieveOrdersSystem(GameConstants gameConstants)
        {
            _enemyBuffer = new Collider[gameConstants.EnemyBufferSize];
        }

        public void Run()
        {
            foreach (var i in _ordersFilter)
            {
                ref var entity = ref _ordersFilter.GetEntity(i);

                ref var orders = ref _ordersFilter.Get1(i);
                ref var movement = ref _ordersFilter.Get2(i);
                ref var entityView = ref entity.Get<ViewComponent>();

                Vector3 originPosition = entityView.View.SelfTransform.position;

                int overlapCounts = Physics.OverlapSphereNonAlloc(originPosition,
                    orders.RadiusReceiveOrders, _enemyBuffer, orders.EnemyLayers);

                if (overlapCounts > 1)
                {
                    float minSqrDistance = (_enemyBuffer[0].transform.position - originPosition).sqrMagnitude;
                    int nearestIndex = 0;

                    for (int j = 1; j < overlapCounts; ++j)
                    {
                        float sqrDistance = (_enemyBuffer[j].transform.position - originPosition).sqrMagnitude;
                        if (sqrDistance < minSqrDistance)
                        {
                            minSqrDistance = sqrDistance;
                            nearestIndex = j;
                        }
                    }

                    if (_enemyBuffer[nearestIndex].TryGetComponent<ActorView>(out var actorView))
                    {
                        orders.NearTarget = actorView;
                    }else
                    {
                        orders.NearTarget = null;
                    }
                }else if (overlapCounts == 1)
                {
                    if (_enemyBuffer[0].TryGetComponent<ActorView>(out var actorView))
                    {
                        orders.NearTarget = actorView;
                    }else
                    {
                        orders.NearTarget = null;
                    }
                }else
                {
                    orders.NearTarget = null;
                }
            }
        }
    }
}