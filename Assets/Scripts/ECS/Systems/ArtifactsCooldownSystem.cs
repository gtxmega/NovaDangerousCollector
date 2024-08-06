using ECS.Components.Artifacts;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
    public class ArtifactsCooldownSystem : IEcsRunSystem
    {
        private EcsFilter<ArtifactCooldownComponent, ArtifactWidgetComponent> _artifactsFilter;

        public void Run()
        {
            foreach (var i in _artifactsFilter)
            {
                ref var artifactCooldownComponent = ref _artifactsFilter.Get1(i);

                if (artifactCooldownComponent.IsReady == false)
                {
                    ref var artifactWidgetComponent = ref _artifactsFilter.Get2(i);

                    artifactCooldownComponent.CooldownTimer -= Time.fixedDeltaTime;

                    float cooldownTime = artifactCooldownComponent.CooldownTime;
                    float cooldownTimer = artifactCooldownComponent.CooldownTimer;
                    artifactWidgetComponent.Widget.UpdateCooldownProgress(cooldownTimer, cooldownTime);

                    if (cooldownTimer <= 0.0f)
                    {
                        artifactCooldownComponent.IsReady = true;
                    }
                }
            }
        }
    }
}