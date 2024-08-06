using ECS.Components;
using ECS.Components.Artifacts;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
    public class DiademOfMadnessSystem : IEcsRunSystem
    {
        private EcsFilter<DiademOfMadnessComponent, ArtifactComponent> _diademFilter;

        public void Run()
        {
            foreach (var i in _diademFilter)
            {
                ref var diademOfMadness = ref _diademFilter.Get1(i);
                ref var artifactComponent = ref _diademFilter.Get2(i);

                ref var targetHealth = ref artifactComponent.Owner.Get<HealthComponent>();

                float totalDecreaseHealth = diademOfMadness.DecreaseHealth;

                if (artifactComponent.Artifact.IsActive)
                {
                    if (diademOfMadness.Duration > 0.0f)
                    {
                        totalDecreaseHealth *= diademOfMadness.Multiplier;
                        diademOfMadness.Duration -= Time.fixedDeltaTime;
                    }
                    else
                    {
                        artifactComponent.Artifact.Deactivate(in artifactComponent.Owner);
                    }
                }

                targetHealth.Current -= totalDecreaseHealth * Time.fixedDeltaTime;
            }
        }
    }
}