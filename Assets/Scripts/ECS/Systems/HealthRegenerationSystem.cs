using ECS.Components;
using ECS.Mark;
using Leopotam.Ecs;
using UnityEngine;

public class HealthRegenerationSystem : IEcsRunSystem
{

    private EcsFilter<HealthComponent>.Exclude<DiedMark> _healthFilter;

    public void Run()
    {
        foreach (var i in _healthFilter)
        {
            ref var healthComponent = ref _healthFilter.Get1(i);

            healthComponent.Current += healthComponent.Regeneration * Time.fixedDeltaTime;
        }
    }
}
