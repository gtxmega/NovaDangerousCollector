using ECS.Components;
using ECS.Mark;
using Game;
using Leopotam.Ecs;
using Logics.Views;
using UnityEngine;

namespace ECS.Systems
{
    public class SearchEnemySystem : IEcsRunSystem
    {
        private EcsFilter<WeaponComponent, WeaponAttributesComponent> _weaponFilter;

        private Collider[] _enemyBuffer;

        private readonly GameConstants _gameConstants;

        public SearchEnemySystem(GameConstants gameConstants)
        {
            _gameConstants = gameConstants;
            _enemyBuffer = new Collider[_gameConstants.EnemyBufferSize];
        }

        public void Run()
        {
            foreach (var i in _weaponFilter)
            {
                ref var weapon = ref _weaponFilter.Get1(i);

                if (weapon.IsReady == false) continue;

                ref var weaponAttributes = ref _weaponFilter.Get2(i);

                Vector3 positionOne = weapon.View.SelfTransform.position;

                int overlapCounts = Physics.OverlapSphereNonAlloc(positionOne,
                                                    weaponAttributes.RadiusOrder, _enemyBuffer, weapon.TargetLayers);

                if (overlapCounts > 1)
                {
                    float minSqrDistance = (_enemyBuffer[0].transform.position - positionOne).sqrMagnitude;
                    int nearesIndex = 0;

                    for (int j = 1; j < overlapCounts; ++j)
                    {
                        float sqrDistance = (_enemyBuffer[j].transform.position - positionOne).sqrMagnitude;
                        if (sqrDistance < minSqrDistance)
                        {
                            minSqrDistance = sqrDistance;
                            nearesIndex = j;
                        }
                    }

                    if (_enemyBuffer[nearesIndex].TryGetComponent<ActorView>(out var targetActor))
                    {
                        weapon.TargetActor = targetActor;
                    }
                    else
                    {
                        weapon.TargetActor = null;
                    }

                }
                else if (overlapCounts == 1)
                {
                    if (_enemyBuffer[0].TryGetComponent<ActorView>(out var targetActor))
                    {
                        weapon.TargetActor = targetActor;
                    }
                    else
                    {
                        weapon.TargetActor = null;
                    }
                }
                else
                {
                    weapon.TargetActor = null;
                }

            }
        }
    }
}