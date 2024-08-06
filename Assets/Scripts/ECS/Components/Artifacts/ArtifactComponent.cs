using Leopotam.Ecs;
using Logics.Artifacts;

namespace ECS.Components.Artifacts
{
    public struct ArtifactComponent
    {
        public EcsEntity Owner;
        public Artifact Artifact;
    }
}