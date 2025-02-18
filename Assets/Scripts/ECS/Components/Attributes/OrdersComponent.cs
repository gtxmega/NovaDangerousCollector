
using Logics.Views;
using UnityEngine;

namespace ECS.Components.Attributes
{
    public struct OrdersComponent
    {
        public ActorView NearTarget;
        public Vector3 SpawnPosition;
        public float RadiusReceiveOrders;
        public float ChaseDistance;
        public LayerMask EnemyLayers;
    }
}