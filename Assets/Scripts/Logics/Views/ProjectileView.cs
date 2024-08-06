using ECS.Components;
using ECS.Mark;
using Leopotam.Ecs;
using UnityEngine;

namespace Logics.Views
{
    public class ProjectileView : EntityView
    {
        private bool _isDied;

        private void OnTriggerEnter(Collider other)
        {
            if (_isDied) return;

            if (other.TryGetComponent<ActorView>(out var actorView))
            {
                ref var projectile = ref _entity.Get<ProjectileComponent>();
                ref var unions = ref _entity.Get<UnionsComponent>();

                bool targetIsEnemy = false;

                for (int i = 0; i < unions.EnemyTeams.Length; ++i)
                {
                    if (unions.EnemyTeams[i] == actorView.Team)
                        targetIsEnemy = true;
                }

                if (targetIsEnemy)
                {
                    actorView.ApplyDamage(projectile.Damage);
                    _entity.Get<DiedMark>();
                    _isDied = true;
                }
            }
        }
    }
}