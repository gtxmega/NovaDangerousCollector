using ECS.Components.Artifacts;
using ECS.Mark;
using Leopotam.Ecs;
using Services.Events;
using UnityEngine;

namespace ECS.Systems.Initialize
{
    public class InitializeArtifacSystem : IEcsRunSystem
    {
        private readonly ILevelEventsExec _levelEventsExec;

        private EcsFilter<ArtifactComponent, SpawnMark> _artifactFilter;

        public InitializeArtifacSystem(ILevelEventsExec levelEventsExec)
        {
            _levelEventsExec = levelEventsExec;
        }

        public void Run()
        {
            foreach (var i in _artifactFilter)
            {
                ref var artifactComponent = ref _artifactFilter.Get1(i);

                artifactComponent.Artifact.ApplyPassiveBonusTo(in artifactComponent.Owner);
                _levelEventsExec.OnPlayerReceivesArtifact(artifactComponent.Artifact);
            }
        }
    }
}