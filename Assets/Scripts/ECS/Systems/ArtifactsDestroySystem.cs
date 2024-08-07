using ECS.Components.Artifacts;
using ECS.Mark;
using Leopotam.Ecs;

namespace ECS.Systems
{
    public class ArtifactsDestroySystem : IEcsRunSystem
    {
        private EcsFilter<ArtifactComponent> _artifactFilter;

        public void Run()
        {
            foreach (var idx in _artifactFilter)
            {
                ref var artifact = ref _artifactFilter.Get1(idx);

                if(artifact.Owner.Has<DiedMark>())
                {
                    ref var artifactEntity = ref _artifactFilter.GetEntity(idx);
                    artifactEntity.Get<DiedMark>();
                }
            }
        }
    }
}