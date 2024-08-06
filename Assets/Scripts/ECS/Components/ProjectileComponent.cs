using Game.Types;
using Leopotam.Ecs;
using Logics.Views;
using UnityEngine;

namespace ECS.Components
{
    public struct ProjectileComponent
    {
        public EcsEntity Owner;
        public ProjectileView View;

        public ActorView Target;

        public bool IsTargetMove;
        public Vector3 EndPosition;

        public float Speed;
        public Damage Damage;
    }
}