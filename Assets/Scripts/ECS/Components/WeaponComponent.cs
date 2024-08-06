using Game.Types;
using Leopotam.Ecs;
using Logics.Views;
using Services.Factory.Builders;
using UnityEngine;

namespace ECS.Components
{
    public struct WeaponComponent
    {
        public EcsEntity Owner;
        public WeaponView View;

        public EAttackType AttackType;
        public LayerMask TargetLayers;

        public bool IsReady;

        public ActorView TargetActor;

        public Transform ProjectileSocket;
        public ProjectileConfig ProjectileConfig;
        public ProjectileConfig CriticalProjectileConfig;
    }
}