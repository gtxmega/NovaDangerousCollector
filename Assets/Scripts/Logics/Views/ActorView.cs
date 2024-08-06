using ECS.Components;
using Game.Types;
using Leopotam.Ecs;
using UnityEngine;

namespace Logics.Views
{
    public class ActorView : EntityView
    {
        [field: SerializeField] public Transform[] WeaponSockets { get; private set; }

        public Transform GetFreeWeaponSocket()
        {
            for (int i = 0; i < WeaponSockets.Length; ++i)
            {
                if (WeaponSockets[i].childCount == 0 && WeaponSockets[i] != null)
                {
                    return WeaponSockets[i];
                }
            }

            return null;
        }

        public virtual void ApplyDamage(Damage damage)
        {
            if (_entity.IsAlive() && _entity.Has<DamageableComponent>())
            {
                ref var damageableComponent = ref _entity.Get<DamageableComponent>();
                damageableComponent.DamageQueue.Enqueue(damage);
            }
        }
    }
}