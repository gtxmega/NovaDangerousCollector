using ECS.Components;
using Leopotam.Ecs;
using Logics.Views;
using UnityEngine;

namespace Services.Factory.Builders
{
    public class WeaponBuilder
    {
        protected WeaponView _view;
        protected EcsEntity _entity;
        protected EcsEntity _ownerEntity;
        protected EcsWorld _world;

        private Transform _socket;
        private readonly WeaponConfig _weaponConfig;

        public WeaponBuilder(WeaponConfig weaponConfig) => _weaponConfig = weaponConfig;

        public void SetOwner(in EcsEntity ownerEntity) => _ownerEntity = ownerEntity;
        public void SetWorld(EcsWorld world) => _world = world;
        public void SetWeaponSocket(Transform socket) => _socket = socket;

        public virtual void Make()
        {
            _entity = _world.NewEntity();
            _view = Object.Instantiate(_weaponConfig.WeaponPrefab, _socket);

            ref var weaponComponent = ref _entity.Get<WeaponComponent>();

            weaponComponent.Owner = _ownerEntity;
            weaponComponent.View = _view;
            weaponComponent.AttackType = _weaponConfig.AttackType;
            weaponComponent.ProjectileSocket = _view.ProjectileSocket;
            weaponComponent.ProjectileConfig = _weaponConfig.ProjectileConfig;
            weaponComponent.CriticalProjectileConfig = _weaponConfig.CriticalProjectileConfig;

            ref var weaponAttributesComponent = ref _entity.Get<WeaponAttributesComponent>();
            weaponAttributesComponent.Damage = _weaponConfig.Damage;
            weaponAttributesComponent.DamageType = _weaponConfig.DamageType;
            weaponAttributesComponent.Reloads = _weaponConfig.Reloads;
            weaponAttributesComponent.ReloadsSpeed = _weaponConfig.ReloadsSpeed;
            weaponAttributesComponent.RadiusOrder = _weaponConfig.RadiusOrder;
            weaponAttributesComponent.Vampirism = _weaponConfig.Vampirism;
            weaponAttributesComponent.CriticalChance = _weaponConfig.CriticalChance;
            weaponAttributesComponent.CriticalDamage = _weaponConfig.CriticalDamage;

            ref var unionsComponent = ref _ownerEntity.Get<UnionsComponent>();
            weaponComponent.TargetLayers = unionsComponent.EnemyLayers;

            _view.Init(in _entity, _world);

            weaponComponent.IsReady = true;
        }

        public WeaponView GetView() => _view;

        public ref EcsEntity GetResult() => ref _entity;

    }
}